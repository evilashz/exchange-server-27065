using System;
using System.ComponentModel;
using System.DirectoryServices;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E5 RID: 741
	[Cmdlet("test", "ExchangeServersWriteAccess")]
	public sealed class TestExchangeServersWriteAccess : SetupTaskBase
	{
		// Token: 0x060019AA RID: 6570 RVA: 0x0007240A File Offset: 0x0007060A
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.exs = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.ExSWkGuid);
			if (this.exs == null)
			{
				base.WriteVerbose(Strings.VerboseNoExchangeServersUSG);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00072440 File Offset: 0x00070640
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			bool flag2 = false;
			if (this.exs != null)
			{
				RawSecurityDescriptor rawSecurityDescriptor = this.rootDomainRecipientSession.ReadSecurityDescriptor(this.exs.Id);
				if (rawSecurityDescriptor == null)
				{
					bool useGlobalCatalog = this.recipientSession.UseGlobalCatalog;
					this.recipientSession.UseGlobalCatalog = true;
					try
					{
						rawSecurityDescriptor = this.recipientSession.ReadSecurityDescriptor(this.exs.Id);
					}
					finally
					{
						this.recipientSession.UseGlobalCatalog = useGlobalCatalog;
					}
				}
				if (rawSecurityDescriptor == null)
				{
					base.WriteVerbose(Strings.SecurityDescriptorAccessDeniedException(this.exs.Id.DistinguishedName));
				}
				else
				{
					try
					{
						ActiveDirectoryRights requestedAccess = ActiveDirectoryRights.Delete | ActiveDirectoryRights.ReadControl | ActiveDirectoryRights.WriteDacl | ActiveDirectoryRights.WriteOwner | ActiveDirectoryRights.CreateChild | ActiveDirectoryRights.DeleteChild | ActiveDirectoryRights.ListChildren | ActiveDirectoryRights.Self | ActiveDirectoryRights.ReadProperty | ActiveDirectoryRights.WriteProperty | ActiveDirectoryRights.DeleteTree | ActiveDirectoryRights.ListObject;
						flag2 = TestExchangeServersWriteAccess.AccessCheck(rawSecurityDescriptor, requestedAccess);
						flag = true;
					}
					catch (Win32Exception ex)
					{
						base.WriteWarning(ex.Message);
					}
				}
			}
			if (flag2)
			{
				base.WriteVerbose(Strings.VerboseHasWriteAccessToExchangeServers);
			}
			else if (flag)
			{
				base.WriteVerbose(Strings.VerboseNoWriteAccessToExchangeServers);
			}
			else
			{
				base.WriteVerbose(Strings.VerboseFailedToTestWriteAccessToExchangeServers);
			}
			base.WriteObject(flag2);
			TaskLogger.LogExit();
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00072558 File Offset: 0x00070758
		private static bool AccessCheck(RawSecurityDescriptor rsd, ActiveDirectoryRights requestedAccess)
		{
			byte[] array = new byte[rsd.BinaryLength];
			rsd.GetBinaryForm(array, 0);
			NativeMethods.GENERIC_MAPPING generic_MAPPING;
			generic_MAPPING.GenericRead = 2147483648U;
			generic_MAPPING.GenericWrite = 1073741824U;
			generic_MAPPING.GenericExecute = 536870912U;
			generic_MAPPING.GenericAll = 268435456U;
			uint num = (uint)requestedAccess;
			uint num2 = 0U;
			bool flag = false;
			NativeMethods.MapGenericMask(ref num, ref generic_MAPPING);
			if (!NativeMethods.ImpersonateSelf(2))
			{
				throw new Win32Exception();
			}
			try
			{
				SafeTokenHandle safeTokenHandle;
				if (!NativeMethods.OpenThreadToken(NativeMethods.GetCurrentThread(), TokenAccessLevels.Query, true, out safeTokenHandle))
				{
					throw new Win32Exception();
				}
				using (safeTokenHandle)
				{
					int cb = 1024;
					SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(Marshal.AllocHGlobal(cb));
					using (safeHGlobalHandle)
					{
						if (!NativeMethods.AccessCheckByType(array, IntPtr.Zero, safeTokenHandle, num, IntPtr.Zero, 0, ref generic_MAPPING, safeHGlobalHandle, ref cb, out num2, out flag))
						{
							throw new Win32Exception();
						}
					}
				}
			}
			finally
			{
				NativeMethods.RevertToSelf();
			}
			return num2 == num;
		}

		// Token: 0x04000B1B RID: 2843
		private ADGroup exs;
	}
}
