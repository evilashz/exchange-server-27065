using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigratedFooterSizeExceedsDisclaimerMaxSizeException : LocalizedException
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000CA89 File Offset: 0x0000AC89
		public MigratedFooterSizeExceedsDisclaimerMaxSizeException(string domain, string disclaimer, int actualSize, int maxSize) : base(CoreStrings.MigratedFooterSizeExceedsDisclaimerMaxSize(domain, disclaimer, actualSize, maxSize))
		{
			this.domain = domain;
			this.disclaimer = disclaimer;
			this.actualSize = actualSize;
			this.maxSize = maxSize;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public MigratedFooterSizeExceedsDisclaimerMaxSizeException(string domain, string disclaimer, int actualSize, int maxSize, Exception innerException) : base(CoreStrings.MigratedFooterSizeExceedsDisclaimerMaxSize(domain, disclaimer, actualSize, maxSize), innerException)
		{
			this.domain = domain;
			this.disclaimer = disclaimer;
			this.actualSize = actualSize;
			this.maxSize = maxSize;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		protected MigratedFooterSizeExceedsDisclaimerMaxSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.disclaimer = (string)info.GetValue("disclaimer", typeof(string));
			this.actualSize = (int)info.GetValue("actualSize", typeof(int));
			this.maxSize = (int)info.GetValue("maxSize", typeof(int));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
			info.AddValue("disclaimer", this.disclaimer);
			info.AddValue("actualSize", this.actualSize);
			info.AddValue("maxSize", this.maxSize);
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000CBDD File Offset: 0x0000ADDD
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000CBE5 File Offset: 0x0000ADE5
		public string Disclaimer
		{
			get
			{
				return this.disclaimer;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000CBED File Offset: 0x0000ADED
		public int ActualSize
		{
			get
			{
				return this.actualSize;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000CBF5 File Offset: 0x0000ADF5
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
		}

		// Token: 0x04000355 RID: 853
		private readonly string domain;

		// Token: 0x04000356 RID: 854
		private readonly string disclaimer;

		// Token: 0x04000357 RID: 855
		private readonly int actualSize;

		// Token: 0x04000358 RID: 856
		private readonly int maxSize;
	}
}
