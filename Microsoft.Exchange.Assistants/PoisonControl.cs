using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Win32;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000072 RID: 114
	internal abstract class PoisonControl : Base
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00010A98 File Offset: 0x0000EC98
		public PoisonControl(PoisonControlMaster master, DatabaseInfo databaseInfo, string subKeyContainerName)
		{
			this.master = master;
			this.databaseInfo = databaseInfo;
			this.subKeyDatabaseName = databaseInfo.Guid.ToString() + "\\" + subKeyContainerName;
			if (this.master.RegistryKey == null)
			{
				return;
			}
			ExTraceGlobals.PoisonControlTracer.TraceDebug<PoisonControl, string, string>((long)this.GetHashCode(), "{0}: Creating registry key '{1}\\{2}'", this, this.master.RegistryKey.Name, this.subKeyDatabaseName);
			this.registryKeyDatabase = this.master.RegistryKey.CreateSubKey(this.subKeyDatabaseName, RegistryKeyPermissionCheck.ReadWriteSubTree);
			DateTime utcNow = DateTime.UtcNow;
			string[] subKeyNames = this.registryKeyDatabase.GetSubKeyNames();
			foreach (string text in subKeyNames)
			{
				ExTraceGlobals.PoisonControlTracer.TraceDebug<PoisonControl, string, string>((long)this.GetHashCode(), "{0}: Opening registry key '{1}\\{2}'", this, this.registryKeyDatabase.Name, text);
				CrashData crashData = CrashData.Read(this.registryKeyDatabase, text);
				if (crashData == null)
				{
					ExTraceGlobals.PoisonControlTracer.TraceDebug<PoisonControl, string, string>((long)this.GetHashCode(), "{0}: Discarding registry key '{1}\\{2}' because it has no useful data", this, this.registryKeyDatabase.Name, text);
					this.RemoveSubKey(text);
				}
				else if (utcNow - crashData.Time > PoisonControl.MaximumKeyAge)
				{
					ExTraceGlobals.PoisonControlTracer.TraceDebug((long)this.GetHashCode(), "{0}: Discarding registry key '{1}\\{2}' because it is old ({3})", new object[]
					{
						this,
						this.registryKeyDatabase.Name,
						text,
						crashData.Time
					});
					this.RemoveSubKey(text);
				}
				else
				{
					ExTraceGlobals.PoisonControlTracer.TraceDebug<PoisonControl, string, string>((long)this.GetHashCode(), "{0}: Loading registry key '{1}\\{2}'", this, this.registryKeyDatabase.Name, text);
					this.LoadCrashData(text, crashData.Count);
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00010C66 File Offset: 0x0000EE66
		protected DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.databaseInfo;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00010C6E File Offset: 0x0000EE6E
		protected PoisonControlMaster Master
		{
			get
			{
				return this.master;
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00010C94 File Offset: 0x0000EE94
		public void PoisonCall(EmergencyKit kit, TryDelegate dangerousCall)
		{
			PoisonControl.<>c__DisplayClass1 CS$<>8__locals1 = new PoisonControl.<>c__DisplayClass1();
			CS$<>8__locals1.kit = kit;
			CS$<>8__locals1.<>4__this = this;
			ILUtil.DoTryFilterCatch(dangerousCall, new FilterDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<PoisonCall>b__0)), null);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		protected void SaveCrashData(string subKeyName, int crashCount)
		{
			CrashData.Write(this.registryKeyDatabase, subKeyName, crashCount);
		}

		// Token: 0x0600035E RID: 862
		protected abstract void HandleUnhandledException(object exception, EmergencyKit kit);

		// Token: 0x0600035F RID: 863
		protected abstract void LoadCrashData(string subKeyName, int crashCount);

		// Token: 0x06000360 RID: 864 RVA: 0x00010CD8 File Offset: 0x0000EED8
		protected void RemoveDatabaseKey()
		{
			if (this.registryKeyDatabase != null)
			{
				ExTraceGlobals.PoisonControlTracer.TraceDebug<PoisonControl, string>((long)this.GetHashCode(), "{0}: Deleting registry key named '{1}'", this, this.registryKeyDatabase.Name);
				this.TryDeleteRegistrySubKey(this.Master.RegistryKey, this.subKeyDatabaseName);
				this.registryKeyDatabase = this.master.RegistryKey.CreateSubKey(this.subKeyDatabaseName, RegistryKeyPermissionCheck.ReadWriteSubTree);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00010D43 File Offset: 0x0000EF43
		private void RemoveSubKey(string subKeyName)
		{
			if (this.registryKeyDatabase != null)
			{
				ExTraceGlobals.PoisonControlTracer.TraceDebug<PoisonControl, string, string>((long)this.GetHashCode(), "{0}: Deleting registry key named '{1}\\{2}'", this, this.registryKeyDatabase.Name, subKeyName);
				this.TryDeleteRegistrySubKey(this.registryKeyDatabase, subKeyName);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00010D80 File Offset: 0x0000EF80
		private void TryDeleteRegistrySubKey(RegistryKey registryKey, string subKeyName)
		{
			Exception ex = null;
			try
			{
				registryKey.DeleteSubKeyTree(subKeyName);
			}
			catch (ArgumentNullException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			catch (SecurityException ex5)
			{
				ex = ex5;
			}
			catch (ObjectDisposedException ex6)
			{
				ex = ex6;
			}
			catch (UnauthorizedAccessException ex7)
			{
				ex = ex7;
			}
			if (ex != null)
			{
				ExTraceGlobals.PoisonControlTracer.TraceError((long)this.GetHashCode(), "{0}: Unable to delete registry key named '{1}\\{2}' due to exception: {3}", new object[]
				{
					this,
					registryKey.Name,
					subKeyName,
					ex
				});
			}
		}

		// Token: 0x040001EA RID: 490
		private static readonly TimeSpan MaximumKeyAge = TimeSpan.FromHours(24.0);

		// Token: 0x040001EB RID: 491
		private DatabaseInfo databaseInfo;

		// Token: 0x040001EC RID: 492
		private RegistryKey registryKeyDatabase;

		// Token: 0x040001ED RID: 493
		private string subKeyDatabaseName;

		// Token: 0x040001EE RID: 494
		private PoisonControlMaster master;
	}
}
