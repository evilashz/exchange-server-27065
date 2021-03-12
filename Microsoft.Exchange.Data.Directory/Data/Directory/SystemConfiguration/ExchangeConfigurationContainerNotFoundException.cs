using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A91 RID: 2705
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeConfigurationContainerNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FA6 RID: 32678 RVA: 0x001A44BF File Offset: 0x001A26BF
		public ExchangeConfigurationContainerNotFoundException() : base(DirectoryStrings.ExchangeConfigurationContainerNotFoundException)
		{
		}

		// Token: 0x06007FA7 RID: 32679 RVA: 0x001A44CC File Offset: 0x001A26CC
		public ExchangeConfigurationContainerNotFoundException(Exception innerException) : base(DirectoryStrings.ExchangeConfigurationContainerNotFoundException, innerException)
		{
		}

		// Token: 0x06007FA8 RID: 32680 RVA: 0x001A44DA File Offset: 0x001A26DA
		protected ExchangeConfigurationContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FA9 RID: 32681 RVA: 0x001A44E4 File Offset: 0x001A26E4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
