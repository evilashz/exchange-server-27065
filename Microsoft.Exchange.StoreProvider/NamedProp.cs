using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001F7 RID: 503
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NamedProp : IEquatable<NamedProp>, IComparable<NamedProp>
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x000228F4 File Offset: 0x00020AF4
		public NamedProp(Guid guid, string name)
		{
			if (name == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("name");
			}
			this.kind = NamedPropKind.String;
			this.guid = guid;
			if (!this.guid.Equals(WellKnownNamedPropertyGuid.InternetHeaders))
			{
				this.strName = name;
				return;
			}
			bool flag = false;
			FaultInjectionUtils.FaultInjectionTracer.TraceTest<bool>(3964022077U, ref flag);
			if (!flag && !NamedProp.IsValidInternetHeadersName(name))
			{
				throw MapiExceptionHelper.InvalidParameterException("Internet Header name contains non-ASCII characters");
			}
			this.strName = name.ToLowerInvariant();
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00022972 File Offset: 0x00020B72
		public NamedProp(Guid guid, int id)
		{
			this.kind = NamedPropKind.Id;
			this.guid = guid;
			this.id = id;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002298F File Offset: 0x00020B8F
		public NamedProp(NamedProp other)
		{
			this.kind = other.kind;
			this.guid = other.guid;
			this.id = other.id;
			this.strName = other.strName;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000229C8 File Offset: 0x00020BC8
		public static bool IsValidInternetHeadersName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			foreach (char c in name)
			{
				if (c < '!' || c > '~' || c == ':')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00022A14 File Offset: 0x00020C14
		public override int GetHashCode()
		{
			if (this.kind == NamedPropKind.Id)
			{
				return this.guid.GetHashCode() ^ this.id;
			}
			return this.guid.GetHashCode() ^ this.strName.GetHashCode();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00022A54 File Offset: 0x00020C54
		public override bool Equals(object o)
		{
			NamedProp namedProp = o as NamedProp;
			return namedProp != null && this.Equals(namedProp);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00022A74 File Offset: 0x00020C74
		public bool Equals(NamedProp other)
		{
			if (this.kind != other.kind || this.guid != other.guid)
			{
				return false;
			}
			if (this.kind != NamedPropKind.Id)
			{
				return string.Equals(this.strName, other.strName);
			}
			return this.id == other.id;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00022ACC File Offset: 0x00020CCC
		public int CompareTo(NamedProp other)
		{
			if (other == null)
			{
				return -1;
			}
			int num = this.Kind - other.Kind;
			if (num != 0)
			{
				return num;
			}
			num = this.Guid.CompareTo(other.Guid);
			if (num != 0)
			{
				return num;
			}
			if (this.Kind == NamedPropKind.Id)
			{
				return this.Id - other.Id;
			}
			return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00022B33 File Offset: 0x00020D33
		public NamedPropKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00022B3B File Offset: 0x00020D3B
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00022B43 File Offset: 0x00020D43
		public string Name
		{
			get
			{
				if (this.Kind != NamedPropKind.String)
				{
					throw MapiExceptionHelper.IncompatiblePropTypeException("Kind of this named prop is Id and not a String");
				}
				return this.strName;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00022B5F File Offset: 0x00020D5F
		public int Id
		{
			get
			{
				if (this.Kind != NamedPropKind.Id)
				{
					throw MapiExceptionHelper.IncompatiblePropTypeException("Kind of this named prop is String and not an Id");
				}
				return this.id;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00022B7A File Offset: 0x00020D7A
		public virtual bool IsSingleInstanced
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00022B80 File Offset: 0x00020D80
		public override string ToString()
		{
			if (this.Kind == NamedPropKind.Id)
			{
				return "NamedPropGuid=" + this.Guid.ToString() + ", NamedPropId=" + this.Id.ToString();
			}
			return string.Concat(new string[]
			{
				"NamedPropGuid=",
				this.Guid.ToString(),
				", NamedPropName=\"",
				this.Name.ToString(),
				"\""
			});
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00022C11 File Offset: 0x00020E11
		[Obsolete("Directly use the C'tor and use WellKnownNamedProperties to find singletons.")]
		public static NamedProp Create(Guid guid, string name)
		{
			return new NamedProp(guid, name);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00022C1A File Offset: 0x00020E1A
		[Obsolete("Directly use the C'tor and use WellKnownNamedProperties to find singletons.")]
		public static NamedProp Create(Guid guid, int id)
		{
			return new NamedProp(guid, id);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00022C24 File Offset: 0x00020E24
		internal unsafe static NamedProp MarshalFromNative(SNameId* buffer)
		{
			Guid guid = (Guid)Marshal.PtrToStructure(buffer->lpGuid, typeof(Guid));
			NamedProp namedProp;
			if (buffer->ulKind == 0)
			{
				namedProp = new NamedProp(guid, buffer->union.id);
			}
			else
			{
				if (buffer->ulKind != 1)
				{
					throw new ArgumentException("Invalid named property type");
				}
				namedProp = new NamedProp(guid, Marshal.PtrToStringUni(buffer->union.lpStr));
			}
			NamedProp namedProp2 = WellKnownNamedProperties.Find(namedProp);
			if (namedProp2 == null)
			{
				return namedProp;
			}
			return namedProp2;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00022CA4 File Offset: 0x00020EA4
		internal int GetBytesToMarshal()
		{
			int num = SNameId.SizeOf + 7 & -8;
			num += (Marshal.SizeOf(typeof(Guid)) + 7 & -8);
			if (this.Kind == NamedPropKind.String)
			{
				num += ((this.Name.Length + 1) * 2 + 7 & -8);
			}
			return num;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00022CF4 File Offset: 0x00020EF4
		internal unsafe void MarshalToNative(SNameId* pspv, ref byte* pExtra)
		{
			byte* ptr = pExtra;
			pExtra += (IntPtr)(Marshal.SizeOf(typeof(Guid)) + 7 & -8);
			*(Guid*)ptr = this.guid;
			pspv->lpGuid = (IntPtr)((void*)ptr);
			pspv->ulKind = (int)this.Kind;
			if (this.Kind == NamedPropKind.String)
			{
				char* ptr2 = pExtra;
				pspv->union.lpStr = (IntPtr)((void*)ptr2);
				pExtra += (IntPtr)((this.Name.Length + 1) * 2 + 7 & -8);
				foreach (char c in this.Name)
				{
					*(ptr2++) = c;
				}
				*ptr2 = '\0';
				return;
			}
			pspv->union.id = this.Id;
		}

		// Token: 0x040006E4 RID: 1764
		private NamedPropKind kind;

		// Token: 0x040006E5 RID: 1765
		private Guid guid;

		// Token: 0x040006E6 RID: 1766
		private string strName;

		// Token: 0x040006E7 RID: 1767
		private int id;
	}
}
