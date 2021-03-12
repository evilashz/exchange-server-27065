using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E18 RID: 3608
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdminGroupsNotFoundException : LocalizedException
	{
		// Token: 0x0600A57B RID: 42363 RVA: 0x002861B9 File Offset: 0x002843B9
		public AdminGroupsNotFoundException() : base(Strings.AdminGroupsNotFoundException)
		{
		}

		// Token: 0x0600A57C RID: 42364 RVA: 0x002861C6 File Offset: 0x002843C6
		public AdminGroupsNotFoundException(Exception innerException) : base(Strings.AdminGroupsNotFoundException, innerException)
		{
		}

		// Token: 0x0600A57D RID: 42365 RVA: 0x002861D4 File Offset: 0x002843D4
		protected AdminGroupsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A57E RID: 42366 RVA: 0x002861DE File Offset: 0x002843DE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
