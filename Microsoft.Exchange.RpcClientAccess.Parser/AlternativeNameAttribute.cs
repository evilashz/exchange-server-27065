using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.Implementation)]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AlternativeNameAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AlternativeNameAttribute(AlternativeNaming scheme, string name)
		{
			this.Scheme = scheme;
			this.Name = name;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E6 File Offset: 0x000002E6
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020EE File Offset: 0x000002EE
		public AlternativeNaming Scheme { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F7 File Offset: 0x000002F7
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020FF File Offset: 0x000002FF
		public string Name { get; private set; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002174 File Offset: 0x00000374
		internal static ILookup<TEnum, string> GetMappingForEnum<TEnum>(AlternativeNaming scheme)
		{
			if (!typeof(TEnum).IsEnum)
			{
				throw new ArgumentException("Must be of an enumerated type", "TEnum");
			}
			return (from field in typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.Public)
			from attr in 
				from AlternativeNameAttribute attr in Attribute.GetCustomAttributes(field, typeof(AlternativeNameAttribute))
				where attr.Scheme == scheme
				select attr
			select new KeyValuePair<TEnum, string>((TEnum)((object)field.GetRawConstantValue()), attr.Name)).ToLookup((KeyValuePair<TEnum, string> pair) => pair.Key, (KeyValuePair<TEnum, string> pair) => pair.Value);
		}
	}
}
