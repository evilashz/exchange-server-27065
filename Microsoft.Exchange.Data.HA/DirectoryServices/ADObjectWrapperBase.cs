using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ADObjectWrapperBase : IADObjectCommon
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002E6C File Offset: 0x0000106C
		protected ADObjectWrapperBase(ADObject objToWrap)
		{
			this.Identity = objToWrap.Identity;
			this.Id = objToWrap.Id;
			this.Guid = objToWrap.Guid;
			this.Name = objToWrap.Name;
			this.IsValid = objToWrap.IsValid;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002EBC File Offset: 0x000010BC
		protected ADObjectWrapperBase(IADObjectCommon source)
		{
			this.Identity = source.Identity;
			this.Id = source.Id;
			this.Guid = source.Guid;
			this.Name = source.Name;
			this.IsValid = source.IsValid;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002F0B File Offset: 0x0000110B
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002F13 File Offset: 0x00001113
		public ObjectId Identity { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002F1C File Offset: 0x0000111C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002F24 File Offset: 0x00001124
		public ADObjectId Id { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002F2D File Offset: 0x0000112D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002F35 File Offset: 0x00001135
		public Guid Guid { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002F3E File Offset: 0x0000113E
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002F46 File Offset: 0x00001146
		public string Name { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002F4F File Offset: 0x0000114F
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002F57 File Offset: 0x00001157
		public bool IsValid { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F60 File Offset: 0x00001160
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002F68 File Offset: 0x00001168
		protected bool IsMinimized { get; set; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002F71 File Offset: 0x00001171
		public virtual void Minimize()
		{
			this.IsMinimized = true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F7A File Offset: 0x0000117A
		protected void HandleMinimizedProperty(string propName)
		{
			ADObjectWrapperBase.DisableMinimization();
			throw new MinimizedPropertyException(propName);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F87 File Offset: 0x00001187
		protected void CheckMinimizedProperty(string propName)
		{
			if (this.IsMinimized)
			{
				this.HandleMinimizedProperty(propName);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F98 File Offset: 0x00001198
		public static bool IsMinimizeEnabled()
		{
			int num = 0;
			Exception ex = null;
			try
			{
				IRegistryReader instance = RegistryReader.Instance;
				num = instance.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "DisableADObjectMinimize", 0);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			catch (SecurityException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ADObjectWrapperBase.Tracer.TraceError<Exception>(0L, "IsMinimizeEnabled caught {0}", ex);
				num = 1;
			}
			return num == 0;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003020 File Offset: 0x00001220
		public static void DisableMinimization()
		{
			Exception ex = null;
			try
			{
				IRegistryWriter instance = RegistryWriter.Instance;
				instance.SetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "DisableADObjectMinimize", 1, RegistryValueKind.DWord);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			catch (SecurityException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ADObjectWrapperBase.Tracer.TraceError<Exception>(0L, "DisableMinimization caught {0}", ex);
			}
		}

		// Token: 0x0400002C RID: 44
		private const string DisableKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";

		// Token: 0x0400002D RID: 45
		private const string DisableValueName = "DisableADObjectMinimize";

		// Token: 0x0400002E RID: 46
		protected static readonly Trace Tracer = ExTraceGlobals.ActiveManagerClientTracer;
	}
}
