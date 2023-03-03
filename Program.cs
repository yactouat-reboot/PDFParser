using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;

using (PdfDocument document = PdfDocument.Open(@"C:\Users\bla\bla"))
{
    foreach (Page page in document.GetPages())
    {

        /**
         * this method allows to get all parsed words from the PDF as an enumerable,
         * we could derive templates out of this, for instance, for extracting tabular data,
         * just by determining at which indices the given table starts/ends;
         * also we would need to be aware that sometimes in documents, the first page is not formatted the same way
         * 
         * */
        IEnumerable<Word> words = page.GetWords();
        Console.WriteLine(words);

        // this is how you would get a page number =>
        Console.WriteLine(page.Number);

        // get each individual letter metadata
        IReadOnlyList<Letter> letters = page.Letters;
        Console.WriteLine(letters);

        // accessing document blocks and their metadata, here the document is symbolized as one block
        var blocks = DefaultPageSegmenter.Instance.GetBlocks(words);
        Console.WriteLine(blocks);

        // recursive XY cut method (more fine-grained that Docstrum, may be suitable for tables once a pattern is detected)
        var recursiveXYCutBlocks = RecursiveXYCut.Instance.GetBlocks(words);
        Console.WriteLine(recursiveXYCutBlocks);

        // Docstrum bounding boxes method (not suitable for tables, it seems)
        var docstrumBlocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);
        Console.WriteLine(docstrumBlocks);

        // TODO investigate https://github.com/chezou/tabula-py

    }
}
