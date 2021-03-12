using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveWindowsIdentityException : LocalizedException
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000046C8 File Offset: 0x000028C8
		public CannotResolveWindowsIdentityException() : base(Strings.CannotResolveWindowsIdentityException)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000046D5 File Offset: 0x000028D5
		public CannotResolveWindowsIdentityException(Exception innerException) : base(Strings.CannotResolveWindowsIdentityException, innerException)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000046E3 File Offset: 0x000028E3
		protected CannotResolveWindowsIdentityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000046ED File Offset: 0x000028ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
