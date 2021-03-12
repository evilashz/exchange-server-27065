using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002AD RID: 685
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveParentException : LocalizedException
	{
		// Token: 0x060018D7 RID: 6359 RVA: 0x0005C675 File Offset: 0x0005A875
		public CannotResolveParentException(string ou) : base(Strings.CannotResolveParentOrganization(ou))
		{
			this.ou = ou;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0005C68A File Offset: 0x0005A88A
		public CannotResolveParentException(string ou, Exception innerException) : base(Strings.CannotResolveParentOrganization(ou), innerException)
		{
			this.ou = ou;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0005C6A0 File Offset: 0x0005A8A0
		protected CannotResolveParentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ou = (string)info.GetValue("ou", typeof(string));
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0005C6CA File Offset: 0x0005A8CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ou", this.ou);
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0005C6E5 File Offset: 0x0005A8E5
		public string Ou
		{
			get
			{
				return this.ou;
			}
		}

		// Token: 0x04000988 RID: 2440
		private readonly string ou;
	}
}
