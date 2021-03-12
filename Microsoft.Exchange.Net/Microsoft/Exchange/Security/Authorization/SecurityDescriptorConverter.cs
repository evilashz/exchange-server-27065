using System;
using System.DirectoryServices;
using System.Security.AccessControl;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200064D RID: 1613
	public static class SecurityDescriptorConverter
	{
		// Token: 0x06001D3D RID: 7485 RVA: 0x00035648 File Offset: 0x00033848
		public static ActiveDirectorySecurity ConvertToActiveDirectorySecurity(RawSecurityDescriptor rawSd)
		{
			if (rawSd == null)
			{
				throw new ArgumentNullException("RawSecurityDescriptor");
			}
			ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
			byte[] array = new byte[rawSd.BinaryLength];
			rawSd.GetBinaryForm(array, 0);
			activeDirectorySecurity.SetSecurityDescriptorBinaryForm(array);
			return activeDirectorySecurity;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00035685 File Offset: 0x00033885
		public static RawSecurityDescriptor ConvertToRawSecurityDescriptor(ActiveDirectorySecurity activeDirectorySecurity)
		{
			if (activeDirectorySecurity == null)
			{
				throw new ArgumentNullException("ActiveDirectorySecurity");
			}
			return new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0);
		}
	}
}
