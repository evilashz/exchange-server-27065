using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x020000F1 RID: 241
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceDataContractSerializationBehavior : DataContractSerializerOperationBehavior
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x00014B2D File Offset: 0x00012D2D
		public LoadBalanceDataContractSerializationBehavior(OperationDescription operation) : base(operation)
		{
			base.DataContractResolver = new LoadBalanceDataContractResolver();
			base.DataContractSurrogate = new LoadBalanceDataContractSurrogate();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00014B4C File Offset: 0x00012D4C
		public LoadBalanceDataContractSerializationBehavior(OperationDescription operation, DataContractFormatAttribute dataContractFormatAttribute) : base(operation, dataContractFormatAttribute)
		{
			base.DataContractResolver = new LoadBalanceDataContractResolver();
			base.DataContractSurrogate = new LoadBalanceDataContractSurrogate();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00014B6C File Offset: 0x00012D6C
		public override XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
		{
			return new DataContractSerializer(type, name, ns, knownTypes, int.MaxValue, false, true, base.DataContractSurrogate, base.DataContractResolver);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00014B98 File Offset: 0x00012D98
		public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
		{
			return new DataContractSerializer(type, name, ns, knownTypes, int.MaxValue, false, true, base.DataContractSurrogate, base.DataContractResolver);
		}
	}
}
