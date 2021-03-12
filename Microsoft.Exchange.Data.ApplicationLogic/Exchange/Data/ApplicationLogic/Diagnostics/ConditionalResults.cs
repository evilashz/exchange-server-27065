using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000D5 RID: 213
	internal class ConditionalResults
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x00024573 File Offset: 0x00022773
		public ConditionalResults(BaseConditionalRegistration registration, RegistrationResult result, Dictionary<PropertyDefinition, object> data)
		{
			this.Completed = (ExDateTime)TimeProvider.UtcNow;
			this.Registration = registration;
			this.Result = result;
			this.Data = data;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000245A0 File Offset: 0x000227A0
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x000245A8 File Offset: 0x000227A8
		public BaseConditionalRegistration Registration { get; private set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000245B1 File Offset: 0x000227B1
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x000245B9 File Offset: 0x000227B9
		public ExDateTime Completed { get; private set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000245C2 File Offset: 0x000227C2
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x000245CA File Offset: 0x000227CA
		public RegistrationResult Result { get; private set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000245D3 File Offset: 0x000227D3
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x000245DB File Offset: 0x000227DB
		public Dictionary<PropertyDefinition, object> Data { get; private set; }

		// Token: 0x0600092E RID: 2350 RVA: 0x000245E4 File Offset: 0x000227E4
		public XElement GetXmlResults()
		{
			XElement xelement = new XElement("Registration");
			XElement xelement2 = new XElement("CreationDate");
			xelement.Add(xelement2);
			xelement2.Add(new XText(this.Registration.Created.ToString()));
			XElement xelement3 = new XElement("Description");
			xelement.Add(xelement3);
			ConditionalRegistration conditionalRegistration = this.Registration as ConditionalRegistration;
			if (conditionalRegistration != null)
			{
				xelement3.Add(new XCData(conditionalRegistration.Description));
			}
			else
			{
				xelement3.Add(new XCData((this.Registration as PersistentConditionalRegistration).Cookie));
			}
			XElement xelement4 = new XElement("OriginalFilter");
			xelement.Add(xelement4);
			xelement4.Add(new XCData(this.Registration.OriginalFilter));
			XElement xelement5 = new XElement("ParsedFilter");
			xelement.Add(xelement5);
			xelement5.Add(new XCData(this.Registration.QueryFilter.ToString()));
			XElement xelement6 = new XElement("Fetch");
			xelement.Add(xelement6);
			xelement6.Add(new XText(this.Registration.OriginalPropertiesToFetch));
			ConditionalRegistration conditionalRegistration2 = this.Registration as ConditionalRegistration;
			if (conditionalRegistration2 != null)
			{
				XElement xelement7 = new XElement("MaxHits");
				xelement.Add(xelement7);
				xelement7.Add(new XText(conditionalRegistration2.MaxHits.ToString()));
			}
			XElement xelement8 = new XElement("Results");
			xelement.Add(xelement8);
			xelement8.Add(new XText(this.Result.ToString()));
			XElement xelement9 = new XElement("CompleteDate");
			xelement.Add(xelement9);
			xelement9.Add(new XText(this.Completed.ToString()));
			XElement xelement10 = new XElement("Data");
			xelement.Add(xelement10);
			if (this.Data == null)
			{
				xelement10.Add(new XCData("NO DATA"));
			}
			else
			{
				foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in this.Data)
				{
					XElement xelement11 = new XElement(keyValuePair.Key.Name);
					xelement10.Add(xelement11);
					if (keyValuePair.Value is string || keyValuePair.Value.GetType().IsValueType)
					{
						xelement11.Add(new XCData(keyValuePair.Value.ToString()));
					}
					else
					{
						XmlSerializer xmlSerializer = new XmlSerializer(keyValuePair.Value.GetType());
						using (MemoryStream memoryStream = new MemoryStream())
						{
							xmlSerializer.Serialize(memoryStream, keyValuePair.Value);
							memoryStream.Position = 0L;
							StreamReader streamReader = new StreamReader(memoryStream);
							xelement11.Add(new XCData(streamReader.ReadToEnd()));
							streamReader.Close();
						}
					}
				}
			}
			return xelement;
		}
	}
}
