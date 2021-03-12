using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x0200005B RID: 91
	public class AssertOperation : DalProbeOperation
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000F16D File Offset: 0x0000D36D
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000F175 File Offset: 0x0000D375
		[XmlAttribute]
		public string ActualValue { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000F17E File Offset: 0x0000D37E
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000F186 File Offset: 0x0000D386
		[XmlAttribute]
		public string ExpectedValue { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000F18F File Offset: 0x0000D38F
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000F197 File Offset: 0x0000D397
		[XmlAttribute]
		public AssertOperator Operator { get; set; }

		// Token: 0x06000260 RID: 608 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public override void Execute(IDictionary<string, object> variables)
		{
			object value = DalProbeOperation.GetValue(this.ActualValue, variables);
			object obj = DalProbeOperation.GetValue(this.ExpectedValue, variables);
			if (value != null && obj != null && obj.GetType() != value.GetType())
			{
				obj = Convert.ChangeType(obj, value.GetType());
			}
			if ((this.Operator == AssertOperator.EqualTo || this.Operator == AssertOperator.LessThanOrEqualTo || this.Operator == AssertOperator.GreaterThanOrEqualTo) && object.Equals(value, obj))
			{
				return;
			}
			if (this.Operator == AssertOperator.NotEqualTo && !object.Equals(value, obj))
			{
				return;
			}
			if (this.Operator == AssertOperator.LessThan || this.Operator == AssertOperator.LessThanOrEqualTo)
			{
				IComparable comparable = value as IComparable;
				if (comparable != null)
				{
					if (comparable.CompareTo(obj) < 0)
					{
						return;
					}
				}
				else
				{
					comparable = (obj as IComparable);
					if (comparable == null)
					{
						throw new ArgumentException(string.Format("Either {0} or {1} must be IComparable", value, obj));
					}
					if (comparable.CompareTo(value) > 0)
					{
						return;
					}
				}
			}
			if (this.Operator == AssertOperator.GreaterThan || this.Operator == AssertOperator.GreaterThanOrEqualTo)
			{
				IComparable comparable2 = value as IComparable;
				if (comparable2 != null)
				{
					if (comparable2.CompareTo(obj) > 0)
					{
						return;
					}
				}
				else
				{
					comparable2 = (obj as IComparable);
					if (comparable2 == null)
					{
						throw new ArgumentException(string.Format("Either {0} or {1} must be IComparable", value, obj));
					}
					if (comparable2.CompareTo(value) < 0)
					{
						return;
					}
				}
			}
			if (this.Operator == AssertOperator.RegexMatch)
			{
				Regex regex = new Regex(obj.ToString());
				if (regex.IsMatch(value.ToString()))
				{
					return;
				}
			}
			throw new ExAssertException(string.Format("Assertion of {0} {1} {2} Failed", value, this.Operator, obj));
		}
	}
}
