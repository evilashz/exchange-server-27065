using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004A2 RID: 1186
	[XmlType(TypeName = "LogConfig")]
	[Serializable]
	public class LogConfigXML : XMLSerializableBase
	{
		// Token: 0x06003636 RID: 13878 RVA: 0x000D51F6 File Offset: 0x000D33F6
		public LogConfigXML()
		{
			this.MaxAge = LogConfigXML.DefaultMaxAge;
			this.MaxDirectorySize = LogConfigXML.DefaultMaxDirectorySize;
			this.MaxFileSize = LogConfigXML.DefaultMaxFileSize;
			this.Enabled = true;
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000D5226 File Offset: 0x000D3426
		// (set) Token: 0x06003638 RID: 13880 RVA: 0x000D522E File Offset: 0x000D342E
		[XmlIgnore]
		internal EnhancedTimeSpan MaxAge { get; set; }

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000D5238 File Offset: 0x000D3438
		// (set) Token: 0x0600363A RID: 13882 RVA: 0x000D5253 File Offset: 0x000D3453
		[XmlAttribute(AttributeName = "MA")]
		public long MaxAgeTicks
		{
			get
			{
				return this.MaxAge.Ticks;
			}
			set
			{
				this.MaxAge = EnhancedTimeSpan.FromTicks(value);
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000D5261 File Offset: 0x000D3461
		// (set) Token: 0x0600363C RID: 13884 RVA: 0x000D5269 File Offset: 0x000D3469
		[XmlIgnore]
		internal Unlimited<ByteQuantifiedSize> MaxDirectorySize { get; set; }

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000D5272 File Offset: 0x000D3472
		// (set) Token: 0x0600363E RID: 13886 RVA: 0x000D527F File Offset: 0x000D347F
		[XmlAttribute(AttributeName = "MDS")]
		public ulong MaxDirectorySizeLong
		{
			get
			{
				return XMLSerializableBase.UnlimitedSizeToUlong(this.MaxDirectorySize);
			}
			set
			{
				this.MaxDirectorySize = XMLSerializableBase.UlongToUnlimitedSize(value);
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x000D528D File Offset: 0x000D348D
		// (set) Token: 0x06003640 RID: 13888 RVA: 0x000D5295 File Offset: 0x000D3495
		[XmlIgnore]
		internal Unlimited<ByteQuantifiedSize> MaxFileSize { get; set; }

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x000D529E File Offset: 0x000D349E
		// (set) Token: 0x06003642 RID: 13890 RVA: 0x000D52AB File Offset: 0x000D34AB
		[XmlAttribute(AttributeName = "MFS")]
		public ulong MaxFileSizeLong
		{
			get
			{
				return XMLSerializableBase.UnlimitedSizeToUlong(this.MaxFileSize);
			}
			set
			{
				this.MaxFileSize = XMLSerializableBase.UlongToUnlimitedSize(value);
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x000D52B9 File Offset: 0x000D34B9
		// (set) Token: 0x06003644 RID: 13892 RVA: 0x000D52C1 File Offset: 0x000D34C1
		[XmlIgnore]
		internal LocalLongFullPath Path { get; set; }

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06003645 RID: 13893 RVA: 0x000D52CA File Offset: 0x000D34CA
		// (set) Token: 0x06003646 RID: 13894 RVA: 0x000D52E7 File Offset: 0x000D34E7
		[XmlAttribute(AttributeName = "PATH")]
		public string PathString
		{
			get
			{
				if (!(this.Path != null))
				{
					return null;
				}
				return this.Path.PathName;
			}
			set
			{
				this.Path = ((value != null) ? LocalLongFullPath.Parse(value) : null);
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000D52FB File Offset: 0x000D34FB
		// (set) Token: 0x06003648 RID: 13896 RVA: 0x000D5303 File Offset: 0x000D3503
		[XmlAttribute(AttributeName = "ENA")]
		public bool Enabled { get; set; }

		// Token: 0x04002498 RID: 9368
		internal static readonly EnhancedTimeSpan DefaultMaxAge = EnhancedTimeSpan.FromDays(7.0);

		// Token: 0x04002499 RID: 9369
		internal static readonly Unlimited<ByteQuantifiedSize> DefaultMaxDirectorySize = ByteQuantifiedSize.FromMB(200UL);

		// Token: 0x0400249A RID: 9370
		internal static readonly Unlimited<ByteQuantifiedSize> DefaultMaxFileSize = ByteQuantifiedSize.FromMB(10UL);
	}
}
