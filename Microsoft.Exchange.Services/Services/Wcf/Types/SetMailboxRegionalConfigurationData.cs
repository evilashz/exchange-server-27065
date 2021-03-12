using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A8D RID: 2701
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxRegionalConfigurationData : OptionsPropertyChangeTracker
	{
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x001063CE File Offset: 0x001045CE
		// (set) Token: 0x06004C65 RID: 19557 RVA: 0x001063D6 File Offset: 0x001045D6
		[DataMember]
		public string DateFormat
		{
			get
			{
				return this.dateFormat;
			}
			set
			{
				this.dateFormat = value;
				base.TrackPropertyChanged("DateFormat");
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06004C66 RID: 19558 RVA: 0x001063EA File Offset: 0x001045EA
		// (set) Token: 0x06004C67 RID: 19559 RVA: 0x001063F2 File Offset: 0x001045F2
		[DataMember]
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
				base.TrackPropertyChanged("Language");
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06004C68 RID: 19560 RVA: 0x00106406 File Offset: 0x00104606
		// (set) Token: 0x06004C69 RID: 19561 RVA: 0x0010640E File Offset: 0x0010460E
		[DataMember]
		public bool LocalizeDefaultFolderName
		{
			get
			{
				return this.localizeDefaultFolderName;
			}
			set
			{
				this.localizeDefaultFolderName = value;
				base.TrackPropertyChanged("LocalizeDefaultFolderName");
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06004C6A RID: 19562 RVA: 0x00106422 File Offset: 0x00104622
		// (set) Token: 0x06004C6B RID: 19563 RVA: 0x0010642A File Offset: 0x0010462A
		[DataMember]
		public string TimeFormat
		{
			get
			{
				return this.timeFormat;
			}
			set
			{
				this.timeFormat = value;
				base.TrackPropertyChanged("TimeFormat");
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x0010643E File Offset: 0x0010463E
		// (set) Token: 0x06004C6D RID: 19565 RVA: 0x00106446 File Offset: 0x00104646
		[DataMember]
		public string TimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
				base.TrackPropertyChanged("TimeZone");
			}
		}

		// Token: 0x04002B40 RID: 11072
		private string dateFormat;

		// Token: 0x04002B41 RID: 11073
		private string language;

		// Token: 0x04002B42 RID: 11074
		private bool localizeDefaultFolderName;

		// Token: 0x04002B43 RID: 11075
		private string timeFormat;

		// Token: 0x04002B44 RID: 11076
		private string timeZone;
	}
}
