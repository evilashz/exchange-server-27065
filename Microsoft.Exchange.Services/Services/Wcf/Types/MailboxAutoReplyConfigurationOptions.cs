using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ABC RID: 2748
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MailboxAutoReplyConfigurationOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06004DF2 RID: 19954 RVA: 0x001076A5 File Offset: 0x001058A5
		// (set) Token: 0x06004DF3 RID: 19955 RVA: 0x001076AD File Offset: 0x001058AD
		[DataMember]
		public OofState AutoReplyState
		{
			get
			{
				return this.autoReplyState;
			}
			set
			{
				this.autoReplyState = value;
				base.TrackPropertyChanged("AutoReplyState");
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x001076C1 File Offset: 0x001058C1
		// (set) Token: 0x06004DF5 RID: 19957 RVA: 0x001076C9 File Offset: 0x001058C9
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
				base.TrackPropertyChanged("EndTime");
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x001076DD File Offset: 0x001058DD
		// (set) Token: 0x06004DF7 RID: 19959 RVA: 0x001076E5 File Offset: 0x001058E5
		[DataMember]
		public ExternalAudience ExternalAudience
		{
			get
			{
				return this.externalAudience;
			}
			set
			{
				this.externalAudience = value;
				base.TrackPropertyChanged("ExternalAudience");
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06004DF8 RID: 19960 RVA: 0x001076F9 File Offset: 0x001058F9
		// (set) Token: 0x06004DF9 RID: 19961 RVA: 0x00107701 File Offset: 0x00105901
		[DataMember]
		public string ExternalMessage
		{
			get
			{
				return this.externalMessage;
			}
			set
			{
				if (value != this.externalMessage)
				{
					this.externalMessage = value;
					this.ExternalMessageText = DataConversionUtils.ConvertHtmlToText(this.externalMessage);
				}
				base.TrackPropertyChanged("ExternalMessage");
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x00107734 File Offset: 0x00105934
		// (set) Token: 0x06004DFB RID: 19963 RVA: 0x0010773C File Offset: 0x0010593C
		[DataMember]
		public string InternalMessage
		{
			get
			{
				return this.internalMessage;
			}
			set
			{
				if (value != this.internalMessage)
				{
					this.internalMessage = value;
					this.InternalMessageText = DataConversionUtils.ConvertHtmlToText(this.internalMessage);
				}
				base.TrackPropertyChanged("InternalMessage");
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x0010776F File Offset: 0x0010596F
		// (set) Token: 0x06004DFD RID: 19965 RVA: 0x00107777 File Offset: 0x00105977
		[DataMember]
		public string ExternalMessageText
		{
			get
			{
				return this.externalMessageText;
			}
			private set
			{
				this.externalMessageText = value;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06004DFE RID: 19966 RVA: 0x00107780 File Offset: 0x00105980
		// (set) Token: 0x06004DFF RID: 19967 RVA: 0x00107788 File Offset: 0x00105988
		[DataMember]
		public string InternalMessageText
		{
			get
			{
				return this.internalMessageText;
			}
			private set
			{
				this.internalMessageText = value;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06004E00 RID: 19968 RVA: 0x00107791 File Offset: 0x00105991
		// (set) Token: 0x06004E01 RID: 19969 RVA: 0x00107799 File Offset: 0x00105999
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
				base.TrackPropertyChanged("StartTime");
			}
		}

		// Token: 0x04002BD7 RID: 11223
		private OofState autoReplyState;

		// Token: 0x04002BD8 RID: 11224
		private string endTime;

		// Token: 0x04002BD9 RID: 11225
		private ExternalAudience externalAudience;

		// Token: 0x04002BDA RID: 11226
		private string externalMessage;

		// Token: 0x04002BDB RID: 11227
		private string internalMessage;

		// Token: 0x04002BDC RID: 11228
		private string externalMessageText;

		// Token: 0x04002BDD RID: 11229
		private string internalMessageText;

		// Token: 0x04002BDE RID: 11230
		private string startTime;
	}
}
