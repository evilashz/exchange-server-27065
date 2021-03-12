using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A5D RID: 2653
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuitabilityDirectoryException : SuitabilityException
	{
		// Token: 0x06007ECD RID: 32461 RVA: 0x001A39DD File Offset: 0x001A1BDD
		public SuitabilityDirectoryException(string fqdn, int error, string errorMessage) : base(DirectoryStrings.SuitabilityDirectoryException(fqdn, error, errorMessage))
		{
			this.fqdn = fqdn;
			this.error = error;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06007ECE RID: 32462 RVA: 0x001A3A02 File Offset: 0x001A1C02
		public SuitabilityDirectoryException(string fqdn, int error, string errorMessage, Exception innerException) : base(DirectoryStrings.SuitabilityDirectoryException(fqdn, error, errorMessage), innerException)
		{
			this.fqdn = fqdn;
			this.error = error;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06007ECF RID: 32463 RVA: 0x001A3A2C File Offset: 0x001A1C2C
		protected SuitabilityDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
			this.error = (int)info.GetValue("error", typeof(int));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06007ED0 RID: 32464 RVA: 0x001A3AA1 File Offset: 0x001A1CA1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
			info.AddValue("error", this.error);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17002EB0 RID: 11952
		// (get) Token: 0x06007ED1 RID: 32465 RVA: 0x001A3ADE File Offset: 0x001A1CDE
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x17002EB1 RID: 11953
		// (get) Token: 0x06007ED2 RID: 32466 RVA: 0x001A3AE6 File Offset: 0x001A1CE6
		public int Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17002EB2 RID: 11954
		// (get) Token: 0x06007ED3 RID: 32467 RVA: 0x001A3AEE File Offset: 0x001A1CEE
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400558A RID: 21898
		private readonly string fqdn;

		// Token: 0x0400558B RID: 21899
		private readonly int error;

		// Token: 0x0400558C RID: 21900
		private readonly string errorMessage;
	}
}
