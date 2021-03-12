using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x0200006B RID: 107
	public class TestReadWriteOperation : DalProbeOperation
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00010A63 File Offset: 0x0000EC63
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00010A6B File Offset: 0x0000EC6B
		[XmlAttribute]
		public DalType Database { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00010A74 File Offset: 0x0000EC74
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00010A7C File Offset: 0x0000EC7C
		[XmlAttribute]
		public string DataType { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00010A85 File Offset: 0x0000EC85
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00010A8D File Offset: 0x0000EC8D
		[XmlAttribute]
		public string OrganizationTag { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00010A96 File Offset: 0x0000EC96
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00010A9E File Offset: 0x0000EC9E
		public XElement DataObject { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00010AA7 File Offset: 0x0000ECA7
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00010AAF File Offset: 0x0000ECAF
		[XmlAttribute]
		public string QueryString { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00010AB8 File Offset: 0x0000ECB8
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		[XmlAttribute]
		public string RootId { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00010AC9 File Offset: 0x0000ECC9
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00010AD1 File Offset: 0x0000ECD1
		[XmlAttribute]
		public string CheckProperties { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00010ADA File Offset: 0x0000ECDA
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		[XmlAttribute]
		public bool MatchAllProperties { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00010AEB File Offset: 0x0000ECEB
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00010AF3 File Offset: 0x0000ECF3
		[XmlAttribute]
		public bool MatchAllObjects { get; set; }

		// Token: 0x060002D8 RID: 728 RVA: 0x00010AFC File Offset: 0x0000ECFC
		public override void Execute(IDictionary<string, object> variables)
		{
			ADObjectDeserializerOperation adobjectDeserializerOperation = new ADObjectDeserializerOperation
			{
				Type = this.DataType,
				DataObject = this.DataObject,
				Return = "$saved"
			};
			adobjectDeserializerOperation.Execute(variables);
			SaveOperation saveOperation = new SaveOperation
			{
				Object = "$saved",
				DataType = this.DataType,
				Database = this.Database,
				OrganizationTag = this.OrganizationTag
			};
			saveOperation.Execute(variables);
			FindOperation findOperation = new FindOperation
			{
				QueryString = this.QueryString,
				RootId = this.RootId,
				DataType = this.DataType,
				Database = this.Database,
				OrganizationTag = this.OrganizationTag,
				Return = "$found"
			};
			findOperation.Execute(variables);
			object value = DalProbeOperation.GetValue("$found", variables);
			IEnumerable foundObjs = this.ValidateObject(variables, value);
			IConfigurable configurable = (IConfigurable)DalProbeOperation.GetValue("$saved", variables);
			this.ValidateProperties(configurable, foundObjs);
			DeleteOperation deleteOperation = new DeleteOperation
			{
				Id = ((ADObjectId)configurable.Identity).ObjectGuid,
				DataType = this.DataType,
				Database = this.Database,
				OrganizationTag = this.OrganizationTag
			};
			deleteOperation.Execute(variables);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00010C68 File Offset: 0x0000EE68
		private IEnumerable ValidateObject(IDictionary<string, object> variables, object foundObj)
		{
			if (foundObj == null)
			{
				throw new DataValidationException(new ObjectValidationError(new LocalizedString(string.Format("Error reading with queryFilter {0}", this.QueryString)), ((IConfigurable)DalProbeOperation.GetValue("$saved", variables)).Identity, this.Database.ToString()));
			}
			IEnumerable enumerable = foundObj as IEnumerable;
			if (enumerable == null)
			{
				enumerable = new object[]
				{
					foundObj
				};
			}
			return enumerable;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010CD8 File Offset: 0x0000EED8
		private void ValidateProperties(object savedObj, IEnumerable foundObjs)
		{
			if (string.IsNullOrWhiteSpace(this.CheckProperties))
			{
				return;
			}
			string[] array = this.CheckProperties.Split(new char[]
			{
				' '
			});
			ValidationError validationError = null;
			foreach (object value in foundObjs)
			{
				validationError = null;
				foreach (string text in array)
				{
					object propertyValue = DalProbeOperation.GetPropertyValue(savedObj, text.Split(new char[]
					{
						'.'
					}), 0);
					object propertyValue2 = DalProbeOperation.GetPropertyValue(value, text.Split(new char[]
					{
						'.'
					}), 0);
					if (object.Equals(propertyValue, propertyValue2))
					{
						if (!this.MatchAllProperties)
						{
							validationError = null;
							break;
						}
					}
					else
					{
						validationError = new ObjectValidationError(new LocalizedString(string.Format("Error comparing property {0}. Value saved is {1}. Value read is {2}", text, propertyValue, propertyValue2)), ((IConfigurable)savedObj).Identity, this.Database.ToString());
						if (this.MatchAllProperties)
						{
							break;
						}
					}
				}
				if (validationError == null && !this.MatchAllObjects)
				{
					return;
				}
				if (validationError != null && this.MatchAllObjects)
				{
					break;
				}
			}
			if (validationError != null)
			{
				throw new DataValidationException(validationError);
			}
		}

		// Token: 0x0400019C RID: 412
		private const string SavedVar = "$saved";

		// Token: 0x0400019D RID: 413
		private const string FoundVar = "$found";
	}
}
