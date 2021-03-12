using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Security.Permissions;

namespace System
{
	// Token: 0x020000C1 RID: 193
	[__DynamicallyInvokable]
	internal class __ComObject : MarshalByRefObject
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x00022C42 File Offset: 0x00020E42
		protected __ComObject()
		{
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00022C4C File Offset: 0x00020E4C
		public override string ToString()
		{
			if (AppDomain.IsAppXModel())
			{
				IStringable stringable = this as IStringable;
				if (stringable != null)
				{
					return stringable.ToString();
				}
			}
			return base.ToString();
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00022C77 File Offset: 0x00020E77
		[SecurityCritical]
		internal IntPtr GetIUnknown(out bool fIsURTAggregated)
		{
			fIsURTAggregated = !base.GetType().IsDefined(typeof(ComImportAttribute), false);
			return Marshal.GetIUnknownForObject(this);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00022C9C File Offset: 0x00020E9C
		internal object GetData(object key)
		{
			object result = null;
			lock (this)
			{
				if (this.m_ObjectToDataMap != null)
				{
					result = this.m_ObjectToDataMap[key];
				}
			}
			return result;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00022CEC File Offset: 0x00020EEC
		internal bool SetData(object key, object data)
		{
			bool result = false;
			lock (this)
			{
				if (this.m_ObjectToDataMap == null)
				{
					this.m_ObjectToDataMap = new Hashtable();
				}
				if (this.m_ObjectToDataMap[key] == null)
				{
					this.m_ObjectToDataMap[key] = data;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00022D54 File Offset: 0x00020F54
		[SecurityCritical]
		internal void ReleaseAllData()
		{
			lock (this)
			{
				if (this.m_ObjectToDataMap != null)
				{
					foreach (object obj in this.m_ObjectToDataMap.Values)
					{
						IDisposable disposable = obj as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
						__ComObject _ComObject = obj as __ComObject;
						if (_ComObject != null)
						{
							Marshal.ReleaseComObject(_ComObject);
						}
					}
					this.m_ObjectToDataMap = null;
				}
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00022E04 File Offset: 0x00021004
		[SecurityCritical]
		internal object GetEventProvider(RuntimeType t)
		{
			object obj = this.GetData(t);
			if (obj == null)
			{
				obj = this.CreateEventProvider(t);
			}
			return obj;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00022E25 File Offset: 0x00021025
		[SecurityCritical]
		internal int ReleaseSelf()
		{
			return Marshal.InternalReleaseComObject(this);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00022E2D File Offset: 0x0002102D
		[SecurityCritical]
		internal void FinalReleaseSelf()
		{
			Marshal.InternalFinalReleaseComObject(this);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00022E38 File Offset: 0x00021038
		[SecurityCritical]
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private object CreateEventProvider(RuntimeType t)
		{
			object obj = Activator.CreateInstance(t, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[]
			{
				this
			}, null);
			if (!this.SetData(t, obj))
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				obj = this.GetData(t);
			}
			return obj;
		}

		// Token: 0x04000464 RID: 1124
		private Hashtable m_ObjectToDataMap;
	}
}
