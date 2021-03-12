using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x0200011E RID: 286
	internal class TransferToVoicemail : KeyMappingBase
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x0002423D File Offset: 0x0002243D
		internal TransferToVoicemail(string context) : base(KeyMappingTypeEnum.TransferToVoicemail, 10, context)
		{
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00024249 File Offset: 0x00022449
		public override bool Validate(IDataValidator validator)
		{
			return true;
		}
	}
}
