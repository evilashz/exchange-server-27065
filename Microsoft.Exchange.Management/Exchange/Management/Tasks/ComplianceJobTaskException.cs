using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F41 RID: 3905
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ComplianceJobTaskException : LocalizedException
	{
		// Token: 0x0600AB3F RID: 43839 RVA: 0x0028EC90 File Offset: 0x0028CE90
		public ComplianceJobTaskException(string failure) : base(Strings.ComplianceJobTaskException(failure))
		{
			this.failure = failure;
		}

		// Token: 0x0600AB40 RID: 43840 RVA: 0x0028ECA5 File Offset: 0x0028CEA5
		public ComplianceJobTaskException(string failure, Exception innerException) : base(Strings.ComplianceJobTaskException(failure), innerException)
		{
			this.failure = failure;
		}

		// Token: 0x0600AB41 RID: 43841 RVA: 0x0028ECBB File Offset: 0x0028CEBB
		protected ComplianceJobTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600AB42 RID: 43842 RVA: 0x0028ECE5 File Offset: 0x0028CEE5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x17003758 RID: 14168
		// (get) Token: 0x0600AB43 RID: 43843 RVA: 0x0028ED00 File Offset: 0x0028CF00
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x040060BE RID: 24766
		private readonly string failure;
	}
}
