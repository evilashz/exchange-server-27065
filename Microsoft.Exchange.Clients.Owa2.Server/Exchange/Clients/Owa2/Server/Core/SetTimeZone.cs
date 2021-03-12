using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000356 RID: 854
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SetTimeZone : ServiceCommand<bool>
	{
		// Token: 0x06001BC0 RID: 7104 RVA: 0x0006ADAA File Offset: 0x00068FAA
		public SetTimeZone(CallContext callContext, string newTimeZone) : base(callContext)
		{
			this.newTimeZone = newTimeZone;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0006ADBC File Offset: 0x00068FBC
		protected override bool InternalExecute()
		{
			UserConfigurationPropertyDefinition propertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.TimeZone);
			new UserOptionsType
			{
				TimeZone = this.newTimeZone
			}.Commit(base.CallContext, new UserConfigurationPropertyDefinition[]
			{
				propertyDefinition
			});
			return true;
		}

		// Token: 0x04000FBF RID: 4031
		private readonly string newTimeZone;
	}
}
