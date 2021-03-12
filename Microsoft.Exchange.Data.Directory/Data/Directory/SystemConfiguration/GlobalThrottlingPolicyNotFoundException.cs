using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000AAC RID: 2732
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlobalThrottlingPolicyNotFoundException : ADExternalException
	{
		// Token: 0x06008022 RID: 32802 RVA: 0x001A4E3E File Offset: 0x001A303E
		public GlobalThrottlingPolicyNotFoundException() : base(DirectoryStrings.GlobalThrottlingPolicyNotFoundException)
		{
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x001A4E4B File Offset: 0x001A304B
		public GlobalThrottlingPolicyNotFoundException(Exception innerException) : base(DirectoryStrings.GlobalThrottlingPolicyNotFoundException, innerException)
		{
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x001A4E59 File Offset: 0x001A3059
		protected GlobalThrottlingPolicyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x001A4E63 File Offset: 0x001A3063
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
