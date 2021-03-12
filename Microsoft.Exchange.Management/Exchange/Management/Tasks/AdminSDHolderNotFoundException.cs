using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E1A RID: 3610
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdminSDHolderNotFoundException : LocalizedException
	{
		// Token: 0x0600A585 RID: 42373 RVA: 0x002862BA File Offset: 0x002844BA
		public AdminSDHolderNotFoundException(string system) : base(Strings.AdminSDHolderNotFoundException(system))
		{
			this.system = system;
		}

		// Token: 0x0600A586 RID: 42374 RVA: 0x002862CF File Offset: 0x002844CF
		public AdminSDHolderNotFoundException(string system, Exception innerException) : base(Strings.AdminSDHolderNotFoundException(system), innerException)
		{
			this.system = system;
		}

		// Token: 0x0600A587 RID: 42375 RVA: 0x002862E5 File Offset: 0x002844E5
		protected AdminSDHolderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.system = (string)info.GetValue("system", typeof(string));
		}

		// Token: 0x0600A588 RID: 42376 RVA: 0x0028630F File Offset: 0x0028450F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("system", this.system);
		}

		// Token: 0x1700363A RID: 13882
		// (get) Token: 0x0600A589 RID: 42377 RVA: 0x0028632A File Offset: 0x0028452A
		public string System
		{
			get
			{
				return this.system;
			}
		}

		// Token: 0x04005FA0 RID: 24480
		private readonly string system;
	}
}
