using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200122E RID: 4654
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidLdapFileNameException : LocalizedException
	{
		// Token: 0x0600BB45 RID: 47941 RVA: 0x002AA02D File Offset: 0x002A822D
		public InvalidLdapFileNameException() : base(Strings.InvalidLdapFileName)
		{
		}

		// Token: 0x0600BB46 RID: 47942 RVA: 0x002AA03A File Offset: 0x002A823A
		public InvalidLdapFileNameException(Exception innerException) : base(Strings.InvalidLdapFileName, innerException)
		{
		}

		// Token: 0x0600BB47 RID: 47943 RVA: 0x002AA048 File Offset: 0x002A8248
		protected InvalidLdapFileNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600BB48 RID: 47944 RVA: 0x002AA052 File Offset: 0x002A8252
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
