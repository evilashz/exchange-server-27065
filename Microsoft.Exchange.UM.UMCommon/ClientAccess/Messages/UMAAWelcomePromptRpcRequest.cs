using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	public class UMAAWelcomePromptRpcRequest : UMAutoAttendantPromptRpcRequest
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x000266CC File Offset: 0x000248CC
		private UMAAWelcomePromptRpcRequest(UMAutoAttendant aa, bool businessHoursFlag, string businessName) : this(aa)
		{
			this.MenuFlag = false;
			if (!string.IsNullOrEmpty(businessName))
			{
				base.AutoAttendant.BusinessName = businessName;
			}
			this.BusinessHoursFlag = businessHoursFlag;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000266F8 File Offset: 0x000248F8
		private UMAAWelcomePromptRpcRequest(UMAutoAttendant aa, bool businessHoursFlag, CustomMenuKeyMapping[] keyMapping) : this(aa)
		{
			this.MenuFlag = true;
			if (keyMapping != null && keyMapping.Length != 0)
			{
				if (businessHoursFlag)
				{
					base.AutoAttendant.BusinessHoursKeyMapping = keyMapping;
					base.AutoAttendant.BusinessHoursKeyMappingEnabled = true;
				}
				else
				{
					base.AutoAttendant.AfterHoursKeyMapping = keyMapping;
					base.AutoAttendant.AfterHoursKeyMappingEnabled = true;
				}
			}
			this.BusinessHoursFlag = businessHoursFlag;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00026761 File Offset: 0x00024961
		private UMAAWelcomePromptRpcRequest(UMAutoAttendant aa) : base(aa)
		{
			this.MenuFlag = false;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00026771 File Offset: 0x00024971
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x00026779 File Offset: 0x00024979
		public bool BusinessHoursFlag { get; private set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00026782 File Offset: 0x00024982
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0002678A File Offset: 0x0002498A
		public bool MenuFlag { get; private set; }

		// Token: 0x06000A12 RID: 2578 RVA: 0x00026793 File Offset: 0x00024993
		public static UMAAWelcomePromptRpcRequest BusinessHoursWithCustomBusinessName(UMAutoAttendant aa, string businessName)
		{
			return new UMAAWelcomePromptRpcRequest(aa, true, businessName);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002679D File Offset: 0x0002499D
		public static UMAAWelcomePromptRpcRequest AfterHoursWithCustomBusinessName(UMAutoAttendant aa, string businessName)
		{
			return new UMAAWelcomePromptRpcRequest(aa, false, businessName);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000267A7 File Offset: 0x000249A7
		public static UMAAWelcomePromptRpcRequest BusinessHoursWithCustomKeyMapping(UMAutoAttendant aa, CustomMenuKeyMapping[] keyMapping)
		{
			return new UMAAWelcomePromptRpcRequest(aa, true, keyMapping);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000267B1 File Offset: 0x000249B1
		public static UMAAWelcomePromptRpcRequest AfterHoursWithCustomKeyMapping(UMAutoAttendant aa, CustomMenuKeyMapping[] keyMapping)
		{
			return new UMAAWelcomePromptRpcRequest(aa, false, keyMapping);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000267BB File Offset: 0x000249BB
		internal override string GetFriendlyName()
		{
			return Strings.AutoAttendantWelcomePromptRequest;
		}
	}
}
