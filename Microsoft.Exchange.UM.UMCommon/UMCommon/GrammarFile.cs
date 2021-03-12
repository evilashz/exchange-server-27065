using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000A2 RID: 162
	internal class GrammarFile
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x0001663C File Offset: 0x0001483C
		public GrammarFile(string path, CultureInfo c)
		{
			ValidateArgument.NotNullOrEmpty(path, "path");
			ValidateArgument.NotNull(c, "c");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "GrammarFile constructor - path='{0}', c='{1}'", new object[]
			{
				path,
				c
			});
			this.grammarWriter = XmlWriter.Create(path);
			this.WriteGrammarFilePrefix(c);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00016698 File Offset: 0x00014898
		public void WriteEntry(ADEntry entry)
		{
			ValidateArgument.NotNull(entry, "entry");
			if (!this.entryWritten)
			{
				this.WriteNamesRulePrefix();
			}
			ExAssert.RetailAssert(entry.Names != null, "entry.Names is null");
			ExAssert.RetailAssert(entry.Names.Count > 0, "entry.Names is empty");
			string text = entry.ObjectGuid.ToString();
			foreach (string text2 in entry.Names)
			{
				ExAssert.RetailAssert(!string.IsNullOrEmpty(text2), "name is '{0}' for objectGuid='{1}'", new object[]
				{
					text2 ?? "<null>",
					text
				});
				PIIMessage data = PIIMessage.Create(PIIType._SmtpAddress, entry.SmtpAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, data, "Writing entry for objectGuid='{0}', SMTP='_SmtpAddress'", new object[]
				{
					text
				});
				this.grammarWriter.WriteRaw(string.Format(CultureInfo.InvariantCulture, "\t\t\t<item>{0}\r\n\t\t\t\t<tag>\r\n\t\t\t\t\t$.ObjectGuid._value=\"{2}\";\r\n\t\t\t\t\t$.SMTP._value=\"{1}\";\r\n\t\t\t\t\t$.ContactName._value=\"{0}\";\r\n\t\t\t\t</tag>\r\n\t\t\t</item>\r\n", new object[]
				{
					text2,
					entry.SmtpAddress,
					text
				}));
			}
			this.entryWritten = true;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000167E4 File Offset: 0x000149E4
		public void Close()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Entering GrammarFile.Close", new object[0]);
			this.WriteGrammarFileSuffix();
			this.grammarWriter.Flush();
			this.grammarWriter.Close();
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00016818 File Offset: 0x00014A18
		private void WriteGrammarFilePrefix(CultureInfo c)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Entering GrammarFile.WriteGrammarFilePrefix", new object[0]);
			this.grammarWriter.WriteStartDocument();
			this.grammarWriter.WriteRaw(string.Format(CultureInfo.InvariantCulture, "\r\n<grammar root=\"Names\" xml:lang=\"{0}\" version=\"1.0\" xmlns=\"http://www.w3.org/2001/06/grammar\" xmlns:sapi=\"http://schemas.microsoft.com/Speech/2002/06/SRGSExtensions\" tag-format=\"semantics-ms/1.0\">", new object[]
			{
				UmCultures.GetGrxmlCulture(c)
			}));
			Utils.CopyPeopleGrammarRules(this.grammarWriter, c);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001687D File Offset: 0x00014A7D
		private void WriteGrammarFileSuffix()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Entering GrammarFile.WriteGrammarFileSuffix", new object[0]);
			if (this.entryWritten)
			{
				this.WriteNamesRuleSuffix();
			}
			else
			{
				this.WriteEmptyNamesRule();
			}
			this.grammarWriter.WriteRaw("\r\n</grammar>");
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000168BB File Offset: 0x00014ABB
		private void WriteNamesRulePrefix()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Entering GrammarFile.WriteNamesRulePrefix", new object[0]);
			this.grammarWriter.WriteRaw("\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={};</tag>\r\n\t\t<tag>$.ResultType={};</tag>\r\n\t\t<tag>$.RecoEvent._value=\"recoNameOrDepartment\";</tag>\r\n\t\t<tag>$.ResultType._value=\"DirectoryContact\";</tag>\r\n\t\t<tag>$.ObjectGuid={};</tag>\r\n\t\t<tag>$.SMTP={};</tag>\r\n\t\t<tag>$.ContactName={};</tag>\r\n");
			this.grammarWriter.WriteRaw("\t\t<one-of>\r\n");
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000168F4 File Offset: 0x00014AF4
		private void WriteNamesRuleSuffix()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Entering GrammarFile.WriteNamesRuleSuffix", new object[0]);
			this.grammarWriter.WriteRaw("\t\t</one-of>\r\n");
			this.grammarWriter.WriteRaw(string.Format(CultureInfo.InvariantCulture, "\r\n\t\t<!-- the following will add an option politeending to the recognition -->\r\n\t\t<item repeat=\"0-1\">\r\n\t\t\t<ruleref uri=\"{0}#politeEndPhrases\"/>\r\n\t\t</item>\r\n\t</rule>", new object[]
			{
				"common.grxml"
			}));
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00016951 File Offset: 0x00014B51
		private void WriteEmptyNamesRule()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Entering GrammarFile.WriteEmptyNamesRule", new object[0]);
			this.grammarWriter.WriteRaw("\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<ruleref special=\"VOID\" />\r\n\t</rule>");
		}

		// Token: 0x04000376 RID: 886
		private bool entryWritten;

		// Token: 0x04000377 RID: 887
		private XmlWriter grammarWriter;
	}
}
