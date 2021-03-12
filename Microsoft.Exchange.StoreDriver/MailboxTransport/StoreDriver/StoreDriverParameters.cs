using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000019 RID: 25
	internal class StoreDriverParameters
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004680 File Offset: 0x00002880
		internal void Load(StoreDriverParameterHandler handler)
		{
			if (handler == null)
			{
				return;
			}
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = Path.Combine(Path.GetDirectoryName(executingAssembly.Location), "StoreDriver.config");
			if (!File.Exists(text))
			{
				StoreDriverParameters.diag.TraceDebug<string>(0L, "The config file {0} does not exist.", text);
				return;
			}
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("StoreDriverParameters.xsd"))
			{
				if (manifestResourceStream == null)
				{
					StoreDriverParameters.diag.TraceDebug<string>(0L, "The schema resource {0} does not exist.", "StoreDriverParameters.xsd");
					return;
				}
				xmlSchemaSet.Add(null, SafeXmlFactory.CreateSafeXmlTextReader(manifestResourceStream));
			}
			xmlSchemaSet.Compile();
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.Schemas = xmlSchemaSet;
			try
			{
				using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					xmlDocument.Load(fileStream);
					XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/configuration/storeDriver/parameters/add");
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode != null && xmlNode is XmlElement)
						{
							this.currentNodeValid = true;
							xmlDocument.Validate(delegate(object sender, ValidationEventArgs args)
							{
								StoreDriverParameters.diag.TraceDebug<string>(0L, "The parameter or value is invalid: {0}.", args.Message);
								this.currentNodeValid = false;
							}, xmlNode);
							if (this.currentNodeValid)
							{
								string value = xmlNode.Attributes.GetNamedItem("key").Value;
								string value2 = xmlNode.Attributes.GetNamedItem("value").Value;
								if (value != null && value2 != null)
								{
									handler(value, value2);
								}
							}
						}
					}
				}
			}
			catch (Exception arg)
			{
				StoreDriverParameters.diag.TraceDebug<Exception>(0L, "StoreDriver encountered an exception {0} when loading parameters from storedriver.config.", arg);
			}
		}

		// Token: 0x0400009C RID: 156
		private const string ConfigXSDName = "StoreDriverParameters.xsd";

		// Token: 0x0400009D RID: 157
		private const string ConfigFileName = "StoreDriver.config";

		// Token: 0x0400009E RID: 158
		private const string ParametersNodePath = "/configuration/storeDriver/parameters/add";

		// Token: 0x0400009F RID: 159
		private static readonly Trace diag = ExTraceGlobals.StoreDriverTracer;

		// Token: 0x040000A0 RID: 160
		private bool currentNodeValid = true;
	}
}
