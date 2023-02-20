using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace FilesAutoOrganizer;

public class GroupByModel
{
    public List<string>? EXT { get; init; }
    public string? FolderDestination { get; init; }
}
public class GroupByFiles
{
    private static List<GroupByModel>? groupbylist = new List<GroupByModel>();
    public GroupByFiles()
    {
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "DOC", "DOCX", "DOTX", "DOT", "ODT", "PPT", "PPTX", "ODS", "ABW","CHM","DOCM","DOTM","EPUB",
                    "MPP","ODF","OTT","XPS","POT","POTX","PPS","PPSX","PPTM","PPTX","PUB","RTF","SDD","SDW","SNP","VSD",
                    "WPS","SXW","WRI","XLS","XLSX","SDC","SXS","XLSM","CHW","CHM"
                },
                FolderDestination = "Document's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "TXT","LOG"
                },
                FolderDestination = "Simple TXT's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "JS","HTML","HTM","C","CPP","JAVA","CSS","CS","PY","H","PHP","ASP","PERL","PL","CLASS","RS","LUA","SH","RB","TS","CC","HH","KT","KTS","GO","PS1"
                    ,"PM","SCALA","SWIFT","PHTML","PHP5","M","PYC","TCL"
                },
                FolderDestination = "Source Code Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "JPG", "BMP", "JPEG", "GIF", "PNG", "ODT", "PSD", "EPS", "ICO","TIFF","PCX","RAW", "WEBP", "CPT", "PIC",
                    "ICON","PSDX","TGA","EMF","WDP", "SVG","XCF","AI","CDR","INDD","CR2","CRW","NEF","PEF"
                },
                FolderDestination = "Image's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "WAV", "WMA", "MP3", "AAC", "OGG", "M4A", "REC","FLAC","MIDI","CDA", "ASF","M4P","M4B","M4A","PCM","ZAB", "AIFF","AMR"
                },
                FolderDestination = "Audio's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "264","3G2","3GP","ARF","ASF","ASX","AVI","BIK","DASH","DVR","FLV","H264","M2T","M2TS","M4V","MKV","MOD","MOV"
                    ,"MP4","MPEG","MPG","MTS","OGV","PRPROJ","RMVB","SWF","VOB","WEBM","WLMP","WMV"
                },
                FolderDestination = "Video's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "EOT","OTF","TTC","TTF","WOFF"
                },
                FolderDestination = "Font's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "OVA","OVF","PVM","VDI","VHD","VHDX","VMDK","VMEM","VMX","VMWAREVM"
                },
                FolderDestination = "VM's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "BAT","EXE","COM","DLL","JAR","SYS","BIN","VBS","MSI","MSIX", "APPLICATION"
                },
                FolderDestination = "Executable's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "RAR","ZIP","7Z","GZ","TAR","TGZ","DEB","RPM","PKG","GZIP","CAB","ZIPX"
                },
                FolderDestination = "Compressed's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "000","ISO","IMG","CCD","CUE","DAA","DAO","NRG","TAO","UIF","VCD"
                },
                FolderDestination = "ISO's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "ACCDB","ACCDT","DB","SQL","SQLITE","MDF","IDX","FDB","CSV","DBF","GDB","MDB","SDF","WDB","XML","JSON"
                },
                FolderDestination = "Database's Folder"
            });
        groupbylist.Add(
            new GroupByModel()
            {
                EXT = new List<string>()
                {
                    "LNK"
                },
                FolderDestination = "Link's Folder"
            });
    }
    public void SerializeAndWriteToFile()
    {
        var filePath = global::System.IO.Path.Combine(global::System.IO.Directory.GetCurrentDirectory(), "appOrganizeSettings.json");
        if (global::System.IO.File.Exists(filePath)) return;
        var jsonString = SerializeObject(groupbylist, Formatting.Indented);
        global::System.IO.File.WriteAllText(filePath, jsonString);
    }

    public void ClearList()
    {
        groupbylist.Clear();
    }
    public void ReadFromFileAndDeserialize()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appOrganizeSettings.json");
        if (!File.Exists(filePath)) return;
        var jsonString = File.ReadAllText(filePath);
        groupbylist = DeserializeObject<List<GroupByModel>>(jsonString);
    }
    public string GetExtFolder(string ext)
    {
        var attempt = groupbylist!.FirstOrDefault(x => x.EXT!.Contains(ext));
        return attempt?.FolderDestination ?? ext;
    }
}