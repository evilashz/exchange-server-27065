using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200011B RID: 283
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class DataContractDiagnosableBase<TArgument> : IDiagnosable where TArgument : DiagnosableArgument, new()
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000212B6 File Offset: 0x0001F4B6
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x000212BE File Offset: 0x0001F4BE
		private protected TArgument Arguments { protected get; private set; }

		// Token: 0x0600083D RID: 2109 RVA: 0x000212C8 File Offset: 0x0001F4C8
		public TObject Deserialize<TObject>(XmlReader reader)
		{
			XmlObjectSerializer xmlObjectSerializer = this.CreateSerializer(typeof(TObject));
			object obj = xmlObjectSerializer.ReadObject(reader);
			return (TObject)((object)obj);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000212F4 File Offset: 0x0001F4F4
		public virtual string GetDiagnosticComponentName()
		{
			return base.GetType().Name;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public virtual XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			this.Arguments = Activator.CreateInstance<TArgument>();
			TArgument arguments = this.Arguments;
			return arguments.RunDiagnosticOperation(delegate
			{
				TArgument arguments2 = this.Arguments;
				arguments2.Initialize(parameters);
				object diagnosticResult = this.GetDiagnosticResult();
				if (diagnosticResult == null)
				{
					XName name = this.GetDiagnosticComponentName();
					TArgument arguments3 = this.Arguments;
					return new XElement(name, new XText(arguments3.GetSupportedArguments()));
				}
				XDocument xdocument = new XDocument();
				using (XmlWriter xmlWriter = xdocument.CreateWriter())
				{
					XmlObjectSerializer xmlObjectSerializer = this.CreateSerializer(diagnosticResult.GetType());
					xmlObjectSerializer.WriteObject(xmlWriter, diagnosticResult);
				}
				return xdocument.Root;
			});
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00021414 File Offset: 0x0001F614
		protected virtual XmlObjectSerializer CreateSerializer(Type type)
		{
			string rootNamespace = type.Namespace ?? string.Empty;
			return new DataContractSerializer(type, type.Name, rootNamespace, Array<Type>.Empty, int.MaxValue, false, true, null, this.CreateDataContractResolver());
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00021451 File Offset: 0x0001F651
		protected virtual DataContractResolver CreateDataContractResolver()
		{
			return null;
		}

		// Token: 0x06000842 RID: 2114
		protected abstract object GetDiagnosticResult();
	}
}
