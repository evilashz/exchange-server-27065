using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FB5 RID: 4021
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AvailabilityConfigReadException : LocalizedException
	{
		// Token: 0x0600AD62 RID: 44386 RVA: 0x00291A41 File Offset: 0x0028FC41
		public AvailabilityConfigReadException(string dn) : base(Strings.AvailabilityConfigReadException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x0600AD63 RID: 44387 RVA: 0x00291A56 File Offset: 0x0028FC56
		public AvailabilityConfigReadException(string dn, Exception innerException) : base(Strings.AvailabilityConfigReadException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x0600AD64 RID: 44388 RVA: 0x00291A6C File Offset: 0x0028FC6C
		protected AvailabilityConfigReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x0600AD65 RID: 44389 RVA: 0x00291A96 File Offset: 0x0028FC96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x170037AB RID: 14251
		// (get) Token: 0x0600AD66 RID: 44390 RVA: 0x00291AB1 File Offset: 0x0028FCB1
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x04006111 RID: 24849
		private readonly string dn;
	}
}
