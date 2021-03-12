using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000095 RID: 149
	[DataContract]
	public class SetMessageFormatConfiguration : SetMessagingConfigurationBase
	{
		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00057826 File Offset: 0x00055A26
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x00057842 File Offset: 0x00055A42
		[DataMember]
		public bool AlwaysShowBcc
		{
			get
			{
				return (bool)(base["AlwaysShowBcc"] ?? false);
			}
			set
			{
				base["AlwaysShowBcc"] = value;
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00057855 File Offset: 0x00055A55
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x00057871 File Offset: 0x00055A71
		[DataMember]
		public bool AlwaysShowFrom
		{
			get
			{
				return (bool)(base["AlwaysShowFrom"] ?? false);
			}
			set
			{
				base["AlwaysShowFrom"] = value;
			}
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00057884 File Offset: 0x00055A84
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x00057896 File Offset: 0x00055A96
		[DataMember]
		public string DefaultFormat
		{
			get
			{
				return (string)base["DefaultFormat"];
			}
			set
			{
				base["DefaultFormat"] = value;
			}
		}

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000578A4 File Offset: 0x00055AA4
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0005790C File Offset: 0x00055B0C
		[DataMember]
		public FormatBarState MessageFont
		{
			get
			{
				return new FormatBarState((string)base["DefaultFontName"], (int)(base["DefaultFontSize"] ?? 2), (FontFlags)(base["DefaultFontFlags"] ?? FontFlags.Normal), (string)base["DefaultFontColor"]);
			}
			set
			{
				base["DefaultFontName"] = value.sFontName;
				base["DefaultFontSize"] = value.iFontSize;
				base["DefaultFontFlags"] = ((value.fBold ? FontFlags.Bold : FontFlags.Normal) | (value.fItalics ? FontFlags.Italic : FontFlags.Normal) | (value.fUnderline ? FontFlags.Underline : FontFlags.Normal));
				base["DefaultFontColor"] = "#" + value.sTextColor;
			}
		}
	}
}
