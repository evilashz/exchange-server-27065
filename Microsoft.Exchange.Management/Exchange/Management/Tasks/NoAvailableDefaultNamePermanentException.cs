using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC5 RID: 3781
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoAvailableDefaultNamePermanentException : ManagementObjectAlreadyExistsException
	{
		// Token: 0x0600A8B1 RID: 43185 RVA: 0x0028A67C File Offset: 0x0028887C
		public NoAvailableDefaultNamePermanentException(string mbx) : base(Strings.ErrorNoAvailableDefaultName(mbx))
		{
			this.mbx = mbx;
		}

		// Token: 0x0600A8B2 RID: 43186 RVA: 0x0028A691 File Offset: 0x00288891
		public NoAvailableDefaultNamePermanentException(string mbx, Exception innerException) : base(Strings.ErrorNoAvailableDefaultName(mbx), innerException)
		{
			this.mbx = mbx;
		}

		// Token: 0x0600A8B3 RID: 43187 RVA: 0x0028A6A7 File Offset: 0x002888A7
		protected NoAvailableDefaultNamePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbx = (string)info.GetValue("mbx", typeof(string));
		}

		// Token: 0x0600A8B4 RID: 43188 RVA: 0x0028A6D1 File Offset: 0x002888D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbx", this.mbx);
		}

		// Token: 0x170036BA RID: 14010
		// (get) Token: 0x0600A8B5 RID: 43189 RVA: 0x0028A6EC File Offset: 0x002888EC
		public string Mbx
		{
			get
			{
				return this.mbx;
			}
		}

		// Token: 0x04006020 RID: 24608
		private readonly string mbx;
	}
}
