using System;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000015 RID: 21
	internal sealed class DiagnosticQueryHandler : StoreDiagnosticInfoHandler
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005122 File Offset: 0x00003322
		private DiagnosticQueryHandler(DiagnosticQueryFactory factory) : base("ManagedStoreQueryHandler")
		{
			this.factory = factory;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005136 File Offset: 0x00003336
		public static DiagnosticQueryHandler Create(DiagnosticQueryFactory factory)
		{
			return new DiagnosticQueryHandler(factory);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005140 File Offset: 0x00003340
		public override XElement GetDiagnosticQuery(DiagnosableParameters parameters)
		{
			XElement result;
			try
			{
				DiagnosticQueryParser parser = this.factory.CreateParser(parameters.Argument);
				DiagnosticQueryRetriever diagnosticQueryRetriever = this.factory.CreateRetriever(parser, parameters);
				DiagnosticQueryXmlFormatter diagnosticQueryXmlFormatter = DiagnosticQueryXmlFormatter.Create(diagnosticQueryRetriever.Results);
				XElement content = diagnosticQueryXmlFormatter.FormatResults();
				result = new XElement(base.GetDiagnosticComponentName(), content);
			}
			catch (DiagnosticQueryException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				XElement content2 = DiagnosticQueryXmlFormatter.FormatException(ex);
				result = new XElement(base.GetDiagnosticComponentName(), content2);
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000051D8 File Offset: 0x000033D8
		internal string Run(string query)
		{
			string result;
			try
			{
				DiagnosticQueryParser parser = this.factory.CreateParser(query);
				DiagnosableParameters parameters = DiagnosableParameters.Create(query, false, false, string.Empty);
				DiagnosticQueryRetriever diagnosticQueryRetriever = this.factory.CreateRetriever(parser, parameters);
				DiagnosticQueryTableFormatter diagnosticQueryTableFormatter = DiagnosticQueryTableFormatter.Create(diagnosticQueryRetriever.Results);
				StringBuilder stringBuilder = diagnosticQueryTableFormatter.FormatResults();
				result = stringBuilder.ToString();
			}
			catch (DiagnosticQueryException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				StringBuilder stringBuilder2 = DiagnosticQueryTableFormatter.FormatException(ex);
				result = stringBuilder2.ToString();
			}
			return result;
		}

		// Token: 0x0400008B RID: 139
		private readonly DiagnosticQueryFactory factory;
	}
}
