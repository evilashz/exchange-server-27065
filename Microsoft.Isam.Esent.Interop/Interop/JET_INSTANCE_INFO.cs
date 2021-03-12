using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200029F RID: 671
	public class JET_INSTANCE_INFO : IEquatable<JET_INSTANCE_INFO>
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001805B File Offset: 0x0001625B
		internal JET_INSTANCE_INFO()
		{
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00018063 File Offset: 0x00016263
		internal JET_INSTANCE_INFO(JET_INSTANCE instance, string instanceName, string[] databases)
		{
			this.hInstanceId = instance;
			this.szInstanceName = instanceName;
			if (databases == null)
			{
				this.cDatabases = 0;
				this.databases = null;
				return;
			}
			this.cDatabases = databases.Length;
			this.databases = new ReadOnlyCollection<string>(databases);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000180A0 File Offset: 0x000162A0
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x000180A8 File Offset: 0x000162A8
		public JET_INSTANCE hInstanceId { get; private set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000180B1 File Offset: 0x000162B1
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x000180B9 File Offset: 0x000162B9
		public string szInstanceName { get; private set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000180C2 File Offset: 0x000162C2
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x000180CA File Offset: 0x000162CA
		public int cDatabases { get; private set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000180D3 File Offset: 0x000162D3
		public IList<string> szDatabaseFileName
		{
			get
			{
				return this.databases;
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000180DB File Offset: 0x000162DB
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_INSTANCE_INFO)obj);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00018104 File Offset: 0x00016304
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_INSTANCE_INFO({0})", new object[]
			{
				this.szInstanceName
			});
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00018134 File Offset: 0x00016334
		public override int GetHashCode()
		{
			int num = this.hInstanceId.GetHashCode() ^ (this.szInstanceName ?? string.Empty).GetHashCode() ^ this.cDatabases << 20;
			for (int i = 0; i < this.cDatabases; i++)
			{
				num ^= this.szDatabaseFileName[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001819C File Offset: 0x0001639C
		public bool Equals(JET_INSTANCE_INFO other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.hInstanceId != other.hInstanceId || this.szInstanceName != other.szInstanceName || this.cDatabases != other.cDatabases)
			{
				return false;
			}
			for (int i = 0; i < this.cDatabases; i++)
			{
				if (this.szDatabaseFileName[i] != other.szDatabaseFileName[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00018218 File Offset: 0x00016418
		internal unsafe void SetFromNativeAscii(NATIVE_INSTANCE_INFO native)
		{
			this.hInstanceId = new JET_INSTANCE
			{
				Value = native.hInstanceId
			};
			this.szInstanceName = Marshal.PtrToStringAnsi(native.szInstanceName);
			this.cDatabases = (int)native.cDatabases;
			string[] array = new string[this.cDatabases];
			for (int i = 0; i < this.cDatabases; i++)
			{
				array[i] = Marshal.PtrToStringAnsi(native.szDatabaseFileName[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)]);
			}
			this.databases = new ReadOnlyCollection<string>(array);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000182B0 File Offset: 0x000164B0
		internal unsafe void SetFromNativeUnicode(NATIVE_INSTANCE_INFO native)
		{
			this.hInstanceId = new JET_INSTANCE
			{
				Value = native.hInstanceId
			};
			this.szInstanceName = Marshal.PtrToStringUni(native.szInstanceName);
			this.cDatabases = (int)native.cDatabases;
			string[] array = new string[this.cDatabases];
			for (int i = 0; i < this.cDatabases; i++)
			{
				array[i] = Marshal.PtrToStringUni(native.szDatabaseFileName[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)]);
			}
			this.databases = new ReadOnlyCollection<string>(array);
		}

		// Token: 0x0400075B RID: 1883
		private ReadOnlyCollection<string> databases;
	}
}
