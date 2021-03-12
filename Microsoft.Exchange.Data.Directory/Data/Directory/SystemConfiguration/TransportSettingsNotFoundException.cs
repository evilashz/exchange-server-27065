using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A94 RID: 2708
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TransportSettingsNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FB3 RID: 32691 RVA: 0x001A4595 File Offset: 0x001A2795
		public TransportSettingsNotFoundException() : base(DirectoryStrings.TransportSettingsNotFoundException)
		{
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x001A45A2 File Offset: 0x001A27A2
		public TransportSettingsNotFoundException(Exception innerException) : base(DirectoryStrings.TransportSettingsNotFoundException, innerException)
		{
		}

		// Token: 0x06007FB5 RID: 32693 RVA: 0x001A45B0 File Offset: 0x001A27B0
		protected TransportSettingsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FB6 RID: 32694 RVA: 0x001A45BA File Offset: 0x001A27BA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
