using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Common.ConfigurableParameters;
using Microsoft.Exchange.Cluster.Common.Registry;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000085 RID: 133
	internal class RegistryStateAccess : StateAccessor, IDisposeTrackable, IDisposable
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0001283D File Offset: 0x00010A3D
		public RegistryStateAccess(string registryKey)
		{
			this.m_registryKeyStr = registryKey;
			this.m_disposeTracker = this.GetDisposeTracker();
			this.m_key = this.TryOpenKey(registryKey);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012865 File Offset: 0x00010A65
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RegistryStateAccess>(this);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001286D File Offset: 0x00010A6D
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00012882 File Offset: 0x00010A82
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_key != null)
				{
					this.m_key.Close();
					this.m_key = null;
				}
				if (this.m_disposeTracker != null)
				{
					this.m_disposeTracker.Dispose();
					this.m_disposeTracker = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000128C4 File Offset: 0x00010AC4
		private IRegistryKey TryOpenKey(string registryKey)
		{
			Exception ex;
			return SharedDependencies.RegistryKeyProvider.TryOpenKey(registryKey, ref ex);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00012910 File Offset: 0x00010B10
		protected override Exception ReadValueInternal<T>(string valueName, T defaultValue, out object value)
		{
			object tmpValue = null;
			value = defaultValue;
			if (this.m_key == null)
			{
				RegistryParameterKeyNotOpenedException ex = new RegistryParameterKeyNotOpenedException(this.m_registryKeyStr);
				return new RegistryParameterReadException(valueName, ex.Message, ex);
			}
			Exception ex2 = RegistryUtil.RunRegistryFunction(delegate()
			{
				tmpValue = this.m_key.GetValue(valueName, defaultValue);
			});
			if (ex2 == null)
			{
				value = tmpValue;
			}
			else
			{
				ex2 = new RegistryParameterReadException(valueName, ex2.Message, ex2);
			}
			return ex2;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000129D0 File Offset: 0x00010BD0
		protected override Exception SetValueInternal(string valueName, string value)
		{
			return this.RunSetValueFunc(valueName, delegate
			{
				this.m_key.SetValue(valueName, value, RegistryValueKind.String);
			});
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00012A3C File Offset: 0x00010C3C
		protected override Exception SetValueInternal(string valueName, int value)
		{
			return this.RunSetValueFunc(valueName, delegate
			{
				this.m_key.SetValue(valueName, value, RegistryValueKind.DWord);
			});
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00012A7C File Offset: 0x00010C7C
		private Exception RunSetValueFunc(string valueName, Action setValueFunc)
		{
			if (this.m_key == null)
			{
				RegistryParameterKeyNotOpenedException ex = new RegistryParameterKeyNotOpenedException(this.m_registryKeyStr);
				return new RegistryParameterWriteException(valueName, ex.Message, ex);
			}
			Exception ex2 = RegistryUtil.RunRegistryFunction(setValueFunc);
			if (ex2 != null)
			{
				ex2 = new RegistryParameterWriteException(valueName, ex2.Message, ex2);
			}
			return ex2;
		}

		// Token: 0x040002AB RID: 683
		private readonly string m_registryKeyStr;

		// Token: 0x040002AC RID: 684
		private IRegistryKey m_key;

		// Token: 0x040002AD RID: 685
		private DisposeTracker m_disposeTracker;
	}
}
