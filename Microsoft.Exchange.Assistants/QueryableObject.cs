using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A4 RID: 164
	internal abstract class QueryableObject : ConfigurableObject
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00019FAA File Offset: 0x000181AA
		public QueryableObject() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00019FB8 File Offset: 0x000181B8
		public Dictionary<string, object> ToDictionary()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (PropertyDefinition propertyDefinition in this.ObjectSchema.AllProperties)
			{
				object obj;
				if (string.Compare(propertyDefinition.Name, "ObjectState", true) != 0 && string.Compare(propertyDefinition.Name, "ExchangeVersion") != 0 && base.TryGetValueWithoutDefault(propertyDefinition, out obj) && obj != null)
				{
					if (typeof(QueryableObject).IsAssignableFrom(propertyDefinition.Type))
					{
						object arg = obj;
						SimpleProviderPropertyDefinition simpleProviderPropertyDefinition = propertyDefinition as SimpleProviderPropertyDefinition;
						if (simpleProviderPropertyDefinition != null)
						{
							if (simpleProviderPropertyDefinition.IsMultivalued)
							{
								List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
								if (QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site1 == null)
								{
									QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(IEnumerable), typeof(QueryableObject)));
								}
								using (IEnumerator enumerator2 = QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site1.Target(QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site1, arg).GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										if (QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site2 == null)
										{
											QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, QueryableObject>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(QueryableObject), typeof(QueryableObject)));
										}
										QueryableObject queryableObject = QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site2.Target(QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site2, enumerator2.Current);
										list.Add(queryableObject.ToDictionary());
									}
								}
								dictionary.Add(propertyDefinition.Name, list);
							}
							else
							{
								if (QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site3 == null)
								{
									QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site3 = CallSite<Action<CallSite, Dictionary<string, object>, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(QueryableObject), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Action<CallSite, Dictionary<string, object>, string, object> target = QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site3.Target;
								CallSite <>p__Site = QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site3;
								Dictionary<string, object> arg2 = dictionary;
								string name = propertyDefinition.Name;
								if (QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site4 == null)
								{
									QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site4 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToDictionary", null, typeof(QueryableObject), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								target(<>p__Site, arg2, name, QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site4.Target(QueryableObject.<ToDictionary>o__SiteContainer0.<>p__Site4, arg));
							}
						}
					}
					else
					{
						dictionary.Add(propertyDefinition.Name, obj);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001A25C File Offset: 0x0001845C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x020000F9 RID: 249
		[CompilerGenerated]
		private static class <ToDictionary>o__SiteContainer0
		{
			// Token: 0x04000427 RID: 1063
			public static CallSite<Func<CallSite, object, IEnumerable>> <>p__Site1;

			// Token: 0x04000428 RID: 1064
			public static CallSite<Func<CallSite, object, QueryableObject>> <>p__Site2;

			// Token: 0x04000429 RID: 1065
			public static CallSite<Action<CallSite, Dictionary<string, object>, string, object>> <>p__Site3;

			// Token: 0x0400042A RID: 1066
			public static CallSite<Func<CallSite, object, object>> <>p__Site4;
		}
	}
}
