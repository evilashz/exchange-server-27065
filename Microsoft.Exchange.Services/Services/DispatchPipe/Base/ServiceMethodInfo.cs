using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.DispatchPipe.Base
{
	// Token: 0x02000DCD RID: 3533
	internal class ServiceMethodInfo
	{
		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060059F6 RID: 23030 RVA: 0x001189DB File Offset: 0x00116BDB
		// (set) Token: 0x060059F7 RID: 23031 RVA: 0x001189E3 File Offset: 0x00116BE3
		internal string Name { get; set; }

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x001189EC File Offset: 0x00116BEC
		// (set) Token: 0x060059F9 RID: 23033 RVA: 0x001189F4 File Offset: 0x00116BF4
		internal Type RequestType { get; set; }

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x060059FA RID: 23034 RVA: 0x001189FD File Offset: 0x00116BFD
		// (set) Token: 0x060059FB RID: 23035 RVA: 0x00118A05 File Offset: 0x00116C05
		internal Type RequestBodyType { get; set; }

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060059FC RID: 23036 RVA: 0x00118A0E File Offset: 0x00116C0E
		// (set) Token: 0x060059FD RID: 23037 RVA: 0x00118A16 File Offset: 0x00116C16
		internal Type ResponseType { get; set; }

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060059FE RID: 23038 RVA: 0x00118A1F File Offset: 0x00116C1F
		// (set) Token: 0x060059FF RID: 23039 RVA: 0x00118A27 File Offset: 0x00116C27
		internal Type ResponseBodyType { get; set; }

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06005A00 RID: 23040 RVA: 0x00118A30 File Offset: 0x00116C30
		// (set) Token: 0x06005A01 RID: 23041 RVA: 0x00118A38 File Offset: 0x00116C38
		internal Type WrappedRequestType { get; set; }

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06005A02 RID: 23042 RVA: 0x00118A41 File Offset: 0x00116C41
		// (set) Token: 0x06005A03 RID: 23043 RVA: 0x00118A49 File Offset: 0x00116C49
		internal Type WrappedResponseType { get; set; }

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06005A04 RID: 23044 RVA: 0x00118A52 File Offset: 0x00116C52
		// (set) Token: 0x06005A05 RID: 23045 RVA: 0x00118A5A File Offset: 0x00116C5A
		internal FieldInfo WrappedResponseBodyField { get; set; }

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06005A06 RID: 23046 RVA: 0x00118A63 File Offset: 0x00116C63
		// (set) Token: 0x06005A07 RID: 23047 RVA: 0x00118A6B File Offset: 0x00116C6B
		internal Dictionary<string, string> WrappedRequestTypeParameterMap { get; set; }

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06005A08 RID: 23048 RVA: 0x00118A74 File Offset: 0x00116C74
		// (set) Token: 0x06005A09 RID: 23049 RVA: 0x00118A7C File Offset: 0x00116C7C
		internal bool IsAsyncPattern { get; set; }

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x06005A0A RID: 23050 RVA: 0x00118A85 File Offset: 0x00116C85
		// (set) Token: 0x06005A0B RID: 23051 RVA: 0x00118A8D File Offset: 0x00116C8D
		internal bool IsAsyncAwait { get; set; }

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06005A0C RID: 23052 RVA: 0x00118A96 File Offset: 0x00116C96
		// (set) Token: 0x06005A0D RID: 23053 RVA: 0x00118A9E File Offset: 0x00116C9E
		internal bool IsWrappedRequest { get; set; }

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06005A0E RID: 23054 RVA: 0x00118AA7 File Offset: 0x00116CA7
		// (set) Token: 0x06005A0F RID: 23055 RVA: 0x00118AAF File Offset: 0x00116CAF
		internal bool IsWrappedResponse { get; set; }

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06005A10 RID: 23056 RVA: 0x00118AB8 File Offset: 0x00116CB8
		// (set) Token: 0x06005A11 RID: 23057 RVA: 0x00118AC0 File Offset: 0x00116CC0
		internal bool IsStreamedResponse { get; set; }

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x06005A12 RID: 23058 RVA: 0x00118AC9 File Offset: 0x00116CC9
		// (set) Token: 0x06005A13 RID: 23059 RVA: 0x00118AD1 File Offset: 0x00116CD1
		internal bool ShouldAutoDisposeRequest { get; set; }

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x00118ADA File Offset: 0x00116CDA
		// (set) Token: 0x06005A15 RID: 23061 RVA: 0x00118AE2 File Offset: 0x00116CE2
		internal bool ShouldAutoDisposeResponse { get; set; }

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x00118AEB File Offset: 0x00116CEB
		// (set) Token: 0x06005A17 RID: 23063 RVA: 0x00118AF3 File Offset: 0x00116CF3
		internal bool IsResponseCacheable { get; set; }

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x06005A18 RID: 23064 RVA: 0x00118AFC File Offset: 0x00116CFC
		// (set) Token: 0x06005A19 RID: 23065 RVA: 0x00118B04 File Offset: 0x00116D04
		internal bool IsHttpGet { get; set; }

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x06005A1A RID: 23066 RVA: 0x00118B0D File Offset: 0x00116D0D
		// (set) Token: 0x06005A1B RID: 23067 RVA: 0x00118B15 File Offset: 0x00116D15
		internal UriTemplate UriTemplate { get; set; }

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06005A1C RID: 23068 RVA: 0x00118B1E File Offset: 0x00116D1E
		// (set) Token: 0x06005A1D RID: 23069 RVA: 0x00118B26 File Offset: 0x00116D26
		internal MethodInfo SyncMethod { get; set; }

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06005A1E RID: 23070 RVA: 0x00118B2F File Offset: 0x00116D2F
		// (set) Token: 0x06005A1F RID: 23071 RVA: 0x00118B37 File Offset: 0x00116D37
		internal MethodInfo BeginMethod { get; set; }

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06005A20 RID: 23072 RVA: 0x00118B40 File Offset: 0x00116D40
		// (set) Token: 0x06005A21 RID: 23073 RVA: 0x00118B48 File Offset: 0x00116D48
		internal MethodInfo EndMethod { get; set; }

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06005A22 RID: 23074 RVA: 0x00118B51 File Offset: 0x00116D51
		// (set) Token: 0x06005A23 RID: 23075 RVA: 0x00118B59 File Offset: 0x00116D59
		internal MethodInfo GenericAsyncTaskMethod { get; set; }

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06005A24 RID: 23076 RVA: 0x00118B62 File Offset: 0x00116D62
		// (set) Token: 0x06005A25 RID: 23077 RVA: 0x00118B6A File Offset: 0x00116D6A
		internal JsonRequestFormat JsonRequestFormat { get; set; }

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06005A26 RID: 23078 RVA: 0x00118B73 File Offset: 0x00116D73
		// (set) Token: 0x06005A27 RID: 23079 RVA: 0x00118B7B File Offset: 0x00116D7B
		internal WebMessageFormat WebMethodResponseFormat { get; set; }

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06005A28 RID: 23080 RVA: 0x00118B84 File Offset: 0x00116D84
		// (set) Token: 0x06005A29 RID: 23081 RVA: 0x00118B8C File Offset: 0x00116D8C
		internal WebMessageFormat WebMethodRequestFormat { get; set; }

		// Token: 0x06005A2A RID: 23082 RVA: 0x00118B98 File Offset: 0x00116D98
		internal SafeXmlSerializer GetOrCreateXmlSerializer(Type type, XmlRootAttribute root)
		{
			SafeXmlSerializer safeXmlSerializer = null;
			if (!this.xmlSerializers.TryGetValue(type, out safeXmlSerializer))
			{
				lock (this.xmlSerializersLock)
				{
					if (!this.xmlSerializers.TryGetValue(type, out safeXmlSerializer))
					{
						safeXmlSerializer = new SafeXmlSerializer(type, root);
						this.xmlSerializers.Add(type, safeXmlSerializer);
					}
				}
			}
			return safeXmlSerializer;
		}

		// Token: 0x040031C1 RID: 12737
		private object xmlSerializersLock = new object();

		// Token: 0x040031C2 RID: 12738
		private Dictionary<Type, SafeXmlSerializer> xmlSerializers = new Dictionary<Type, SafeXmlSerializer>();
	}
}
