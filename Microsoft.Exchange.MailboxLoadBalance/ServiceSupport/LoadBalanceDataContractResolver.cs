using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MigrationWorkflowService;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000F0 RID: 240
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceDataContractResolver : DataContractResolver
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x000149D0 File Offset: 0x00012BD0
		public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
		{
			Type type = knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
			if (type != null)
			{
				return type;
			}
			if (typeNamespace.StartsWith("lb:"))
			{
				string arg = typeNamespace.Split(new char[]
				{
					':'
				}, 2)[1];
				try
				{
					return Type.GetType(string.Format("{0}, {1}", typeName, arg));
				}
				catch (IOException arg2)
				{
					ExTraceGlobals.MailboxLoadBalanceTracer.TraceError<string, string, IOException>(0L, "Failed to load type {0}, {1}: {2}", typeName, arg, arg2);
				}
			}
			return null;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00014A5C File Offset: 0x00012C5C
		public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
		{
			if (!knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace))
			{
				lock (this.dictionaryLock)
				{
					if (!this.typeDictionary.TryLookup(type.FullName, out typeName))
					{
						typeName = this.typeDictionary.Add(type.FullName);
					}
					string value = string.Format("lb:{0}", type.Assembly.FullName);
					if (!this.namespaceDictionary.TryLookup(value, out typeNamespace))
					{
						typeNamespace = this.namespaceDictionary.Add(value);
					}
				}
			}
			return true;
		}

		// Token: 0x040002D0 RID: 720
		private readonly object dictionaryLock = new object();

		// Token: 0x040002D1 RID: 721
		private readonly XmlDictionary typeDictionary = new XmlDictionary();

		// Token: 0x040002D2 RID: 722
		private readonly XmlDictionary namespaceDictionary = new XmlDictionary();
	}
}
