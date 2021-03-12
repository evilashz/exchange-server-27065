using System;
using System.Security.AccessControl;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200010D RID: 269
	internal static class ExchangeSecurityDescriptorHelper
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x00034E28 File Offset: 0x00033028
		internal static RawSecurityDescriptor RemoveInheritedACEs(RawSecurityDescriptor sd)
		{
			if (sd == null)
			{
				return null;
			}
			RawAcl discretionaryAcl = sd.DiscretionaryAcl;
			bool flag = false;
			foreach (GenericAce genericAce in discretionaryAcl)
			{
				if ((byte)(genericAce.AceFlags & AceFlags.Inherited) == 16)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return sd;
			}
			RawAcl rawAcl = new RawAcl(discretionaryAcl.Revision, 0);
			foreach (GenericAce genericAce2 in discretionaryAcl)
			{
				if ((byte)(genericAce2.AceFlags & AceFlags.Inherited) != 16)
				{
					rawAcl.InsertAce(rawAcl.Count, genericAce2);
				}
			}
			return new RawSecurityDescriptor(sd.ControlFlags, sd.Owner, sd.Group, sd.SystemAcl, rawAcl);
		}
	}
}
