function get-rowInner {
        param($inputObject, $unique=0, $trim=0)
 
        $values = @()
        foreach ($obj in $inputObject) {
                if ($obj.nodeName -eq "td" -or $obj.nodeName -eq "th") {
                        $value = $obj.outerText
                        if ($trim) {
                                $value = $value.trim()
                        }
                        if ($unique) {
                                if ($values -contains $value) {
                                        $i = 2
                                        while ($values -contains ($value + $i)) {
                                                $i++
                                        }
                                        $values += ($value + $i)
                                } else {
                                        $values += $value
                                }
                        } else {
                                $values += $value
                        }
                }
        }
 
        if ($values.length -gt 0) {
                return $values
        } else {
                return $null
        }
}      
 
function get-row {
        param($inputObject, $unique=0, $trim=0)
 
        if ($inputObject.nodeName -eq "tr") {
                # We are at the row level.
                return get-rowInner -inputObject $inputObject.childnodes -unique $unique -trim $trim
        } else {
                # Rows can be nested inside other tags.
                foreach ($node in $inputObject.childnodes) {
                        $row = get-row -inputObject $node -unique $unique -trim $trim
                        if ($row -ne $null) {
                                return $row
                        }
                }
        }
}
 
function get-table {
        param($inputObjects)
 
        # We treat the first row as column headings.
        $headings = $null
        $rows = @()
 
        foreach ($obj in $inputObjects) {
                if ($headings -eq $null) {
                        # The first row will be the headings.
                        $headings = get-row -inputObject $obj -unique 1 -trim 1
                        continue
                }
 
                $row = get-row -inputObject $obj
                if ($row -ne $null -and $row.length -eq $headings.length) {
                        $rowObject = new-object psobject
                        for ($i = 0; $i -lt $headings.length; $i++) {
                                $value = $row[$i]
                                if ($value -eq $null) {
                                        $value = ""
                                }
                                $heading = $headings[$i].split("/")[0]
                                $rowObject | add-member -type noteproperty -name $heading -value $value
                        }
                        $rows += $rowObject
                }
        }
 
        return $rows
}
 
function Parse-HtmlTableRecursive {
        param($inputObjects)
 
        foreach ($_ in $inputObjects) {
                if ($_.nodeName -eq "tbody") {
                    return get-table -inputObjects $_.childnodes
                }
 
                if ($_.childnodes -ne $null) {
                        $table = Parse-HtmlTableRecursive -inputObjects $_.childnodes
                        if ($table) {
                                return $table
                        }
                }
        }
 
        return $null
}

# Parse HTML tables and return the rows as PowerShell objects.
function Parse-HtmlTable {
        param($table)
 
        $h = new-object -com "HTMLFILE"
        $h.IHTMLDocument2_write($table)

        $ret = Parse-HtmlTableRecursive -inputObject $h.body
        return $ret
}

###############################################################
##                                                           ##
##                          MAIN                             ##
##                                                           ##
###############################################################


$ie = new-object -com "InternetExplorer.Application"
$ie.visible = $true

$ie.Navigate("https://fortuna.uwaterloo.ca/cgi-bin/cgiwrap/rsic/book/index.html")

# The easiest way to accomodate for slowness of IE
while($ie.busy) {Start-Sleep 1} 

$doc = $ie.Document

$search_book = $doc.getElementByID("search_box_book")
$button = $search_book.getElementsByClassName("search_button").item(0)

$button.click()

# The easiest way to accomodate for slowness of IE
while($ie.busy) {Start-Sleep 1} 

# This will be our collection of parsed objects
$processedBooks = @()

$index = 0

while(1) {
    $URI = "https://fortuna.uwaterloo.ca/cgi-bin/cgiwrap/rsic/book/scan/MM=da86eb2d0180239bac50b59859ed098c:${index}:" + ($index + 9) + ":10.html?mv_more_ip=1&mv_nextpage=results_books%2ehtml&mv_arg="
    $ie.Navigate($URI)
     
    # The easiest way to accomodate for slowness of IE
    while($ie.busy) {Start-Sleep 1}

    #$HTMLCode = $doc.body.outerHTML
    #Set-Content "C:\Users\HyunWoo\Documents\Scripts\testdownload.htm" $HTMLCode

    # Get a collection of book_item elements
    $books = $doc.body.getElementsByClassName("book_item")

    # Total number of books for a specific page
    $books.length

    # If there are no more books, exit
    if(!$books.length) {break}

    # Iterate through the collection
    for ($i=0; $i -lt $books.length; $i++) {
        # Book
        $item = $books.item($i)

        # Found Book
        1
    
        # Get the textbook cover image URL
        $imageURL = $item.getElementsByClassName("cover").item(0).src

        # Get the author
        $author = $item.getElementsByClassName("author").item(0).outerText

        # Get the title
        $title = $item.getElementsByClassName("title").item(0).outerText

        # Get the ISBN
        # raw form "SKU: ISBN#"
        $ISBN = $item.getElementsByClassName("sku").item(0).outerText
        # parse into just ISBN#
        if($ISBN) {
            $id = $ISBN.split(".")[1]

            if($id) {$id = $id.trim()}
            $ISBN = $id
        }

        # Get the BookLook price for this book
        # raw form "Price: #"
        $origPrice = $item.getElementsByClassName("price").item(0).outerText
        # parse into just #
        if($origPrice) {
            $price = $origPrice.split(":")[1]
            
            if($price) {$price = $price.trim()}
            $origPrice = $price
        }

        # Get the related course info
        $courses = $item.getElementsByClassName("course_info").item(0)
        $courses = Parse-HtmlTable -table $courses.outerHTML

        if($courses) {
            $courses = @($courses)
        } else {
            $courses = @()
        }

        ##### TEST: output collected book information to the screen
        "Image URL: " + $imageURL
        "Author: " + $author
        "Title: " + $title
        "ISBN: " + $ISBN
        "Price: $" + $origPrice
        "Courses: "
        for ($j=0; $j -lt $courses.length; $j++) {
            $course = $courses.item($j)
            $course.Course
        }
        #####

        # Below is PowerShell v3 notation. 
        # In v2, replace '[pscustomobject]' with 
        # 'new-object psobject -Property' 
        $obj = [pscustomobject] @{ "imageURL"=$imageURL;
                                   "author"=$author;
                                   "title"=$title;
                                   "ISBN"=$ISBN;
                                   "origPrice"=$origPrice;
                                   "courses"=$courses; }

        $processedBooks += $obj
    }

    $index += 10
}
