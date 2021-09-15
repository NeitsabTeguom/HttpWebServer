using System;
using System.Collections.Generic;
using System.Net;

namespace HttpWebServer
{
    internal class Program
    {
        private static string path = @"C:\tmp";
        public static string SendResponse(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                Console.WriteLine("No client data was sent with the request.");
                return "KO";
            }
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            string ext = "";
            if (request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", request.ContentType);
                ext = GetDefaultExtension(request.ContentType);
            }

            ext = ext == "" ? "dat" : ext;
            Console.WriteLine("Client data content length {0}", request.ContentLength64);

            Console.WriteLine("Start of client data:");
            // Convert the data to a string and display it on the console.
            string s = reader.ReadToEnd();
            try
            {
                string prefix = "";
                if(request.RawUrl != "" && request.RawUrl != "/")
                {
                    prefix = request.RawUrl.Substring(1).Replace("/", "-");
                }
                System.IO.File.WriteAllText(System.IO.Path.Combine(path, prefix + DateTime.Now.Ticks.ToString() + "." + ext), s, encoding);
            }
            catch(Exception ex)
            {

            }
            Console.WriteLine(s);
            Console.WriteLine("End of client data:");
            body.Close();
            reader.Close();

            return "OK";
        }

        private static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                path = args[1];
                var ws = new HttpWebServer(SendResponse, args[0]);
                ws.Run();
                Console.WriteLine("A simple webserver. Press a key to quit.");
                Console.ReadKey();
                ws.Stop();
            }
            else
            {
                Console.WriteLine("First argument with url must be passed (ex : http://localhost:8080/test/).");
                Console.WriteLine("Second argument with path of writing payload must be passed (ex : C:\\tmp\\).");
                Console.ReadKey();
            }
        }

        public static Dictionary<string, string> MimeExts = new Dictionary<string, string>()
        {
            //fichier audio AAC
            { "audio/aac", "aac" },
            //document AbiWord
            { "application/x-abiword", "abw" },
            //archive (contenant plusieurs fichiers)
            //{ "application/octet-stream", "arc" },
            //AVI : Audio Video Interleave
            { "video/x-msvideo", "avi" },
            //format pour eBook Amazon Kindle
            { "application/vnd.amazon.ebook", "azw" },
            //n'importe quelle donnée binaire
            { "application/octet-stream", "bin" },
            //Images bitmap Windows OS/2
            { "image/bmp", "bmp" },
            //archive BZip
            { "application/x-bzip", "bz" },
            //archive BZip2
            { "application/x-bzip2", "bz2" },
            //script C-Shell
            { "application/x-csh", "csh" },
            //fichier Cascading Style Sheets (CSS)
            { "text/css", "css" },
            //fichier Comma-separated values (CSV)
            { "text/csv", "csv" },
            //Microsoft Word
            { "application/msword", "doc" },
            //Microsoft Word (OpenXML)
            { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "docx" },
            //police MS Embedded OpenType
            { "application/vnd.ms-fontobject", "eot" },
            //fichier Electronic publication (EPUB)
            { "application/epub+zip", "epub" },
            //fichier Graphics Interchange Format (GIF)
            { "image/gif", "gif" },
            //fichier HyperText Markup Language (HTML)
            //{ "text/html", "htm
            { "text/html", "html" },
            // Text
            { "text/plain", "txt" },
            //icône   
            { "image/x-icon", "ico" },
            //élément iCalendar
            { "text/calendar", "ics" },
            //archive Java (JAR)
            { "application/java-archive", "jar" },
            //image JPEG
            { "image/jpeg", "jpg" },
            //.jpeg"
            //JavaScript (ECMAScript)
            { "application/javascript", "js" },
            //donnée au format JSON
            { "application/json", "json" },
            //fichier audio Musical Instrument Digital Interface (MIDI)
            { "audio/midi", "mid" },
            //midi"
            //vidéo MPEG  
            { "video/mpeg", "mpeg" },
            //paquet Apple Installer
            { "application/vnd.apple.installer+xml", "mpkg" },
            //présentation OpenDocument
            { "application/vnd.oasis.opendocument.presentation", "odp" },
            //feuille de calcul OpenDocument
            { "application/vnd.oasis.opendocument.spreadsheet", "ods" },
            //document texte OpenDocument
            { "application/vnd.oasis.opendocument.text", "odt" },
            //fichier audio OGG
            { "audio/ogg", "oga" },
            //fichier vidéo OGG
            { "video/ogg", "ogv" },
            //OGG
            { "application/ogg", "ogx" },
            //police OpenType
            { "font/otf", "otf" },
            //fichier Portable Network Graphics
            { "image/png", "png" },
            //Adobe Portable Document Format (PDF)
            { "application/pdf", "pdf" },
            //présentation Microsoft PowerPoint
            { "application/vnd.ms-powerpoint", "ppt" },
            //présentation Microsoft PowerPoint (OpenXML)
            { "application/vnd.openxmlformats-officedocument.presentationml.presentation", "pptx" },
            //archive RAR
            { "application/x-rar-compressed", "rar" },
            //Rich Text Format (RTF)
            { "application/rtf", "rtf" },
            //script shell
            { "application/x-sh", "sh" },
            //fichier Scalable Vector Graphics (SVG)
            { "image/svg+xml", "svg" },
            //fichier Small web format (SWF) ou Adobe Flash
            { "application/x-shockwave-flash", "swf" },
            //fichier d'archive Tape Archive (TAR)
            { "application/x-tar", "tar" },
            //image au format Tagged Image File Format (TIFF)
            { "image/tiff", "tiff" },
            //.tif"
            //fichier Typescript
            { "application/typescript", "ts" },
            //police TrueType
            { "font/ttf", "ttf" },
            //Microsoft Visio
            { "application/vnd.visio", "vsd" },
            //Waveform Audio Format
            { "audio/x-wav", "wav" },
            //fichier audio WEBM
            { "audio/webm", "weba" },
            //fichier vidéo WEBM
            { "video/webm", "webm" },
            //image WEBP
            { "image/webp", "webp" },
            //police Web Open Font Format (WOFF)
            { "font/woff", "woff" },
            //police Web Open Font Format (WOFF)
            { "font/woff2", "woff2" },
            //XHTML
            { "application/xhtml+xml", "xhtml" },
            //Microsoft Excel
            { "application/vnd.ms-excel", "xls" },
            //Microsoft Excel (OpenXML)
            { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "xlsx" },
            //XML
            { "application/xml", "xml" },
            //XUL
            { "application/vnd.mozilla.xul+xml", "xul" },
            //archive ZIP
            { "application/zip", "zip" },
            //conteneur audio/vidéo 3GPP  
            { "video/3gpp", "3gp" },
            { "audio/3gpp", "3gp" },
            //conteneur audio/vidéo 3GPP2
            { "video/3gpp2", "3g2" },
            { "audio/3gpp2", "3g2" },
            //archive 7-zip
            { "application/x-7z-compressed", "7z" }
        };

        public static string GetDefaultExtension(string mimeType)
        {
            /*
            string result;
            RegistryKey key;
            object value;

            key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
            value = key != null ? key.GetValue("Extension", null) : null;
            result = value != null ? value.ToString() : string.Empty;
            
            return result;
            */

            mimeType = mimeType.ToLower();

            return (MimeExts.ContainsKey(mimeType) ? MimeExts[mimeType] : null);
        }
    }
}
