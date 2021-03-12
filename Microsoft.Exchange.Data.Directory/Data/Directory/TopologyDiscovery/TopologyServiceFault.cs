using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006CB RID: 1739
	[DataContract]
	[KnownType(typeof(TopologyServiceFault))]
	[Serializable]
	internal sealed class TopologyServiceFault : IExtensibleDataObject
	{
		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x06005097 RID: 20631 RVA: 0x0012ABB1 File Offset: 0x00128DB1
		// (set) Token: 0x06005098 RID: 20632 RVA: 0x0012ABB9 File Offset: 0x00128DB9
		[DataMember]
		public string Message
		{
			get
			{
				return this.message;
			}
			private set
			{
				this.message = value;
			}
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x0012ABC2 File Offset: 0x00128DC2
		// (set) Token: 0x0600509A RID: 20634 RVA: 0x0012ABCA File Offset: 0x00128DCA
		[DataMember]
		public bool CanRetry
		{
			get
			{
				return this.fCanRetry;
			}
			private set
			{
				this.fCanRetry = value;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x0600509B RID: 20635 RVA: 0x0012ABD3 File Offset: 0x00128DD3
		// (set) Token: 0x0600509C RID: 20636 RVA: 0x0012ABDB File Offset: 0x00128DDB
		[DataMember(EmitDefaultValue = false)]
		public string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			private set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x0600509D RID: 20637 RVA: 0x0012ABE4 File Offset: 0x00128DE4
		// (set) Token: 0x0600509E RID: 20638 RVA: 0x0012ABEC File Offset: 0x00128DEC
		[DataMember(EmitDefaultValue = false)]
		public TopologyServiceFault InnerException
		{
			get
			{
				return this.innerException;
			}
			private set
			{
				this.innerException = value;
			}
		}

		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x0600509F RID: 20639 RVA: 0x0012ABF5 File Offset: 0x00128DF5
		// (set) Token: 0x060050A0 RID: 20640 RVA: 0x0012ABFD File Offset: 0x00128DFD
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionData;
			}
			set
			{
				this.extensionData = value;
			}
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x0012AC08 File Offset: 0x00128E08
		public static TopologyServiceFault Create(Exception ex, bool includeExceptionDetails = false)
		{
			if (ex == null)
			{
				return null;
			}
			TopologyServiceFault topologyServiceFault = new TopologyServiceFault();
			if (ex is LocalizedException)
			{
				topologyServiceFault.Message = ((LocalizedException)ex).LocalizedString;
			}
			else
			{
				topologyServiceFault.Message = new LocalizedString(ex.Message);
			}
			topologyServiceFault.CanRetry = (ex is TransientException);
			topologyServiceFault.StackTrace = (includeExceptionDetails ? ex.StackTrace : string.Empty);
			topologyServiceFault.InnerException = (includeExceptionDetails ? TopologyServiceFault.Create(ex.InnerException, includeExceptionDetails) : null);
			return topologyServiceFault;
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x0012AC94 File Offset: 0x00128E94
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.StackTrace) && this.InnerException == null)
			{
				return this.Message;
			}
			return this.ToStringHelper(false);
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x0012ACBC File Offset: 0x00128EBC
		private string ToStringHelper(bool isInner)
		{
			if (string.IsNullOrEmpty(this.Message))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Message);
			if (this.InnerException != null)
			{
				stringBuilder.AppendFormat(" ----> {0}", this.InnerException.ToStringHelper(true));
			}
			else
			{
				stringBuilder.Append(Environment.NewLine);
			}
			if (!string.IsNullOrEmpty(this.StackTrace))
			{
				stringBuilder.Append(this.StackTrace);
			}
			if (isInner)
			{
				stringBuilder.AppendFormat("{0}-----------{0}", Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040036C6 RID: 14022
		private string message;

		// Token: 0x040036C7 RID: 14023
		private bool fCanRetry;

		// Token: 0x040036C8 RID: 14024
		private string stackTrace;

		// Token: 0x040036C9 RID: 14025
		private TopologyServiceFault innerException;

		// Token: 0x040036CA RID: 14026
		[NonSerialized]
		private ExtensionDataObject extensionData;
	}
}
