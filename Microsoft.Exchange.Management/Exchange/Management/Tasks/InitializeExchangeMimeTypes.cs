﻿using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002C5 RID: 709
	[Cmdlet("initialize", "ExchangeMimeTypes")]
	public sealed class InitializeExchangeMimeTypes : SetupTaskBase
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x0006D750 File Offset: 0x0006B950
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			Organization orgContainer = this.configurationSession.GetOrgContainer();
			MultiValuedProperty<string> mimeTypes = orgContainer.MimeTypes;
			foreach (string item in this.newTypes)
			{
				if (!mimeTypes.Contains(item))
				{
					mimeTypes.Add(item);
				}
			}
			orgContainer.MimeTypes = mimeTypes;
			this.configurationSession.Save(orgContainer);
			TaskLogger.LogExit();
		}

		// Token: 0x04000AE4 RID: 2788
		private readonly string[] newTypes = new string[]
		{
			"application/cdf;cdf",
			"application/directx;x",
			"application/fractals;fif",
			"application/futuresplash;spl",
			"application/hta;hta",
			"application/internet-property-stream;acx",
			"application/java-archive;jar",
			"application/mac-binhex40;hqx",
			"application/msaccess;mdb",
			"application/msaccess;ade",
			"application/msaccess;adp",
			"application/msaccess;mda",
			"application/msaccess;mde",
			"application/msaccess;snp",
			"application/ms-infopath.xml;infopathxml",
			"application/msonenote;one",
			"application/msonenote;onepkg",
			"application/msword;doc",
			"application/msword;dot",
			"application/octet-stream;bin",
			"application/octet-stream;cab",
			"application/octet-stream;chm",
			"application/octet-stream;csv",
			"application/octet-stream;dwp",
			"application/octet-stream;emz",
			"application/octet-stream;eot",
			"application/octet-stream;exe",
			"application/octet-stream;hhk",
			"application/octet-stream;hhp",
			"application/octet-stream;inf",
			"application/octet-stream;java",
			"application/octet-stream;jpb",
			"application/octet-stream;lpk",
			"application/octet-stream;lzh",
			"application/octet-stream;mdp",
			"application/octet-stream;mht",
			"application/octet-stream;mhtml",
			"application/octet-stream;mix",
			"application/octet-stream;msi",
			"application/octet-stream;mso",
			"application/octet-stream;ocx",
			"application/octet-stream;pcx",
			"application/octet-stream;pcz",
			"application/octet-stream;pfb",
			"application/octet-stream;pfm",
			"application/octet-stream;prm",
			"application/octet-stream;prx",
			"application/octet-stream;psd",
			"application/octet-stream;psm",
			"application/octet-stream;psp",
			"application/octet-stream;qxd",
			"application/octet-stream;rar",
			"application/octet-stream;sea",
			"application/octet-stream;smi",
			"application/octet-stream;snp",
			"application/octet-stream;thn",
			"application/octet-stream;toc",
			"application/octet-stream;ttf",
			"application/octet-stream;xsn",
			"application/oda;oda",
			"application/oleobject;ods",
			"application/pdf;pdf",
			"application/pkcs10;p10",
			"application/pkcs7-mime;p7c",
			"application/pkcs7-mime;p7m",
			"application/pkcs7-signature;p7s",
			"application/pkix-cert;cer",
			"application/pkix-crl;crl",
			"application/postscript;ps",
			"application/postscript;ai",
			"application/postscript;eps",
			"application/rtf;rtf",
			"application/vnd.fdf;fdf",
			"application/vnd.ms-excel.addin.macroEnabled.12;xlam",
			"application/vnd.ms-excel.sheet.binary.macroEnabled.12;xlb",
			"application/vnd.ms-excel.sheet.binary.macroEnabled.12;xlsb",
			"application/vnd.ms-excel.sheet.macroEnabled.12;xlsm",
			"application/vnd.ms-excel.template.macroEnabled.12;xltm",
			"application/vnd.ms-excel;xls",
			"application/vnd.ms-excel;dif",
			"application/vnd.ms-excel;xla",
			"application/vnd.ms-excel;xlb",
			"application/vnd.ms-excel;xlc",
			"application/vnd.ms-excel;xlk",
			"application/vnd.ms-excel;xlm",
			"application/vnd.ms-excel;xll",
			"application/vnd.ms-excel;xlt",
			"application/vnd.ms-excel;xlv",
			"application/vnd.ms-excel;xlw",
			"application/vnd.ms-livemeeting;lmf",
			"application/vnd.ms-livemeeting;lmf-ms",
			"application/vnd.ms-officetheme;thmx",
			"application/vnd.ms-package.relationships+xml;rels",
			"application/vnd.ms-pki.certstore;sst",
			"application/vnd.ms-pki.pko;pko",
			"application/vnd.ms-pki.seccat;cat",
			"application/vnd.ms-pki.stl;stl",
			"application/vnd.ms-powerpoint.addin.macroEnabled.12;ppam",
			"application/vnd.ms-powerpoint.presentation.macroEnabled.12;pptm",
			"application/vnd.ms-powerpoint.slide.macroEnabled.12;sldm",
			"application/vnd.ms-powerpoint.slideshow.macroEnabled.12;ppsm",
			"application/vnd.ms-powerpoint.template.macroEnabled.12;potm",
			"application/vnd.ms-powerpoint;ppt",
			"application/vnd.ms-powerpoint;pot",
			"application/vnd.ms-powerpoint;ppa",
			"application/vnd.ms-powerpoint;pps",
			"application/vnd.ms-powerpoint;ppz",
			"application/vnd.ms-powerpoint;pwz",
			"application/vnd.ms-project;mpp",
			"application/vnd.ms-project;mpt",
			"application/vnd.ms-project;mpw",
			"application/vnd.ms-project;mpx",
			"application/vnd.ms-word.document.macroEnabled.12;docm",
			"application/vnd.ms-word.template.macroEnabled.12;dotm",
			"application/vnd.ms-works;wks",
			"application/vnd.ms-works;wcm",
			"application/vnd.ms-works;wdb",
			"application/vnd.ms-works;wps",
			"application/vnd.openxmlformats-officedocument.presentationml.presentation;pptx",
			"application/vnd.openxmlformats-officedocument.presentationml.slide;sldx",
			"application/vnd.openxmlformats-officedocument.presentationml.slideshow;ppsx",
			"application/vnd.openxmlformats-officedocument.presentationml.template;potx",
			"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;xlsx",
			"application/vnd.openxmlformats-officedocument.spreadsheetml.template;xltx",
			"application/vnd.openxmlformats-officedocument.wordprocessingml.document;docx",
			"application/vnd.openxmlformats-officedocument.wordprocessingml.template;dotx",
			"application/vnd.rn-realmedia;rm",
			"application/vnd.visio;vsd",
			"application/vnd.visio;vdx",
			"application/vnd.visio;vsl",
			"application/vnd.visio;vss",
			"application/vnd.visio;vst",
			"application/vnd.visio;vsu",
			"application/vnd.visio;vsw",
			"application/vnd.visio;vsx",
			"application/vnd.visio;vtx",
			"application/vnd.wap.wmlc;wmlc",
			"application/vnd.wap.wmlscriptc;wmlsc",
			"application/winhlp;hlp",
			"application/x-cdf;cdf",
			"application/x-compress;z",
			"application/x-compressed;tgz",
			"application/x-director;dir",
			"application/x-director,dxr",
			"application/x-director;dcr",
			"application/x-gzip;gz",
			"application/x-hdf;hdf",
			"application/x-internet-signup;ins",
			"application/x-internet-signup;isp",
			"application/x-iphone;iii",
			"application/x-java-applet;class",
			"application/x-latex;latex",
			"application/x-mix-transfer;nix",
			"application/xml;xml",
			"application/x-mplayer2;asx",
			"application/x-ms-application;application",
			"application/x-msclip;clp",
			"application/x-msdownload;dll",
			"application/x-msexcel;xls",
			"application/x-ms-manifest;manifest",
			"application/x-msmediaview;mvb",
			"application/x-msmetafile;wmf",
			"application/x-msmoney;mny",
			"application/x-mspowerpoint;ppt",
			"application/x-mspublisher;pub",
			"application/x-ms-reader;lit",
			"application/x-msschedule;scd",
			"application/x-msterminal;trm",
			"application/x-ms-wmd;wmd",
			"application/x-ms-wmz;wmz",
			"application/x-mswrite;wri",
			"application/x-oleobject;hhc",
			"application/x-pdf;pdf",
			"application/x-perfmon;pmr",
			"application/x-perfmon;pma",
			"application/x-perfmon;pmc",
			"application/x-perfmon;pml",
			"application/x-perfmon;pmw",
			"application/x-pkcs12;p12",
			"application/x-pkcs12;pfx",
			"application/x-pkcs7-certificates;p7b",
			"application/x-pkcs7-certificates;spc",
			"application/x-pkcs7-certreqresp;p7r",
			"application/x-shockwave-flash;swf",
			"application/x-shockwave-flash;mfp",
			"application/x-smaf;mmf",
			"application/x-stuffit;sit",
			"application/x-tar;tar",
			"application/x-tex;tex",
			"application/x-troff-man;man",
			"application/x-x509-ca-cert;cer",
			"application/x-zip-compressed;zip",
			"audio/aiff;aif",
			"audio/aiff;aiff",
			"audio/basic;au",
			"audio/basic;snd",
			"audio/mid;mid",
			"audio/mid;rmi",
			"audio/midi;mid",
			"audio/mpeg;mp3",
			"audio/wav;wav",
			"audio/x-aiff;aiff",
			"audio/x-midi;mid",
			"audio/x-mpegurl;m3u",
			"audio/x-ms-wax;wax",
			"audio/x-ms-wma;wma",
			"audio/x-pn-realaudio;ra",
			"audio/x-pn-realaudio;ram",
			"audio/x-smd;smd",
			"audio/x-smd;smx",
			"audio/x-smd;smz",
			"audio/x-wav;wav",
			"drawing/x-dwf;dwf",
			"image/bmp;bmp",
			"image/bmp;dib",
			"image/gif;gif",
			"image/jpeg;jpg",
			"image/jpeg;jpe",
			"image/pjpeg;jpg",
			"image/png;png",
			"image/png;pnz",
			"image/tiff;tif",
			"image/tiff;tiff",
			"image/vnd.ms-modi;mdi",
			"image/vnd.wap.wbmp;wbmp",
			"image/xbm;xbm",
			"image/x-icon;ico",
			"image/x-png;png",
			"image/x-portable-bitmap;pbm",
			"image/x-xbitmap;xbm",
			"image/x-xpixmap;xpm",
			"image/x-xwindowdump;xwd",
			"message/rfc822;eml",
			"message/rfc822;nws",
			"model/vnd.dwf;dwf",
			"text/calendar;ics",
			"text/css;css",
			"text/h323;323",
			"text/html;htm",
			"text/html;html",
			"text/html;hxt",
			"text/iuls;uls",
			"text/javascript;js",
			"text/plain;txt",
			"text/plain;c",
			"text/plain;cpp",
			"text/plain;cs",
			"text/plain;h",
			"text/plain;hxx",
			"text/plain;map",
			"text/plain;vcs",
			"text/plain;xdr",
			"text/richtext;rtx",
			"text/scriptlet;sct",
			"text/scriptlet;wsc",
			"text/sgml;sgml",
			"text/tab-separated-values;tsv",
			"text/vbscript;vbs",
			"text/vnd.wap.wml;wml",
			"text/vnd.wap.wmlscript;wmls",
			"text/webviewhtml;htt",
			"text/x-component;htc",
			"text/x-hdml;hdml",
			"text/xml;xml",
			"text/xml;disco",
			"text/xml;dtd",
			"text/xml;mno",
			"text/xml;vml",
			"text/xml;wdsl",
			"text/xml;xsd",
			"text/xml;xsf",
			"text/xml;xsl",
			"text/xml;xslt",
			"text/x-ms-iqy;iqy",
			"text/x-ms-odc;odc",
			"text/x-ms-rqy;rqy",
			"text/x-vcard;vcf",
			"video/avi;avi",
			"video/mpeg;mpg",
			"video/mpeg;mpa",
			"video/mpeg;mpe",
			"video/mpeg;mpeg",
			"video/mpeg;mpv2",
			"video/msvideo;avi",
			"video/quicktime;mov",
			"video/quicktime;qt",
			"video/x-ivf;ivf",
			"video/x-la-asf;lsf",
			"video/x-la-asf;lsx",
			"video/x-mpeg;mp2",
			"video/x-mpeg2a;mp2",
			"video/x-ms-asf;asf",
			"video/x-ms-asf;asx",
			"video/x-ms-asf;nsc",
			"video/x-msvideo;avi",
			"video/x-ms-wm;wm",
			"video/x-ms-wmp;wmp",
			"video/x-ms-wmv;wmv",
			"video/x-ms-wmx;wmx",
			"video/x-ms-wvx;wvx",
			"x-world/x-vrml;wrl",
			"x-world/x-vrml,xaf",
			"x-world/x-vrml;wrz",
			"x-world/x-vrml;xof"
		};
	}
}
