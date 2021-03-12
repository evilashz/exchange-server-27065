using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000014 RID: 20
	public abstract class WorkDefinition : IWorkData
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x000084E0 File Offset: 0x000066E0
		public WorkDefinition()
		{
			this.Enabled = true;
			this.CreatedTime = DateTime.UtcNow;
			this.StartTime = DateTime.MinValue;
			this.UpdateTime = DateTime.UtcNow;
			this.DeploymentId = Settings.DeploymentId;
			this.TargetResource = string.Empty;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001D6 RID: 470
		// (set) Token: 0x060001D7 RID: 471
		public abstract int Id { get; internal set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D8 RID: 472
		// (set) Token: 0x060001D9 RID: 473
		public abstract string AssemblyPath { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001DA RID: 474
		// (set) Token: 0x060001DB RID: 475
		public abstract string TypeName { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001DC RID: 476
		// (set) Token: 0x060001DD RID: 477
		public abstract string Name { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001DE RID: 478
		// (set) Token: 0x060001DF RID: 479
		public abstract string WorkItemVersion { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001E0 RID: 480
		// (set) Token: 0x060001E1 RID: 481
		[PropertyInformation("The name of the target service.", false)]
		public abstract string ServiceName { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E2 RID: 482
		// (set) Token: 0x060001E3 RID: 483
		public abstract int DeploymentId { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001E4 RID: 484
		// (set) Token: 0x060001E5 RID: 485
		public abstract string ExecutionLocation { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E6 RID: 486
		// (set) Token: 0x060001E7 RID: 487
		public abstract DateTime CreatedTime { get; internal set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E8 RID: 488
		// (set) Token: 0x060001E9 RID: 489
		public abstract bool Enabled { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001EA RID: 490
		// (set) Token: 0x060001EB RID: 491
		public abstract string TargetPartition { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001EC RID: 492
		// (set) Token: 0x060001ED RID: 493
		public abstract string TargetGroup { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001EE RID: 494
		// (set) Token: 0x060001EF RID: 495
		[PropertyInformation("The name of the target resource.", false)]
		public abstract string TargetResource { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001F0 RID: 496
		// (set) Token: 0x060001F1 RID: 497
		public abstract string TargetExtension { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001F2 RID: 498
		// (set) Token: 0x060001F3 RID: 499
		public abstract string TargetVersion { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F4 RID: 500
		// (set) Token: 0x060001F5 RID: 501
		public abstract int RecurrenceIntervalSeconds { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F6 RID: 502
		// (set) Token: 0x060001F7 RID: 503
		[PropertyInformation("The amount of time to wait before assuming the probe is hung.", false)]
		public abstract int TimeoutSeconds { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F8 RID: 504
		// (set) Token: 0x060001F9 RID: 505
		public abstract DateTime StartTime { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001FA RID: 506
		// (set) Token: 0x060001FB RID: 507
		public abstract DateTime UpdateTime { get; internal set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FC RID: 508
		// (set) Token: 0x060001FD RID: 509
		public abstract int MaxRetryAttempts { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001FE RID: 510
		// (set) Token: 0x060001FF RID: 511
		public abstract int CreatedById { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008549 File Offset: 0x00006749
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00008551 File Offset: 0x00006751
		public string DeploymentSourceName { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000855A File Offset: 0x0000675A
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00008562 File Offset: 0x00006762
		public DateTime? LastExecutionStartTime { get; internal set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000856B File Offset: 0x0000676B
		public Dictionary<string, string> Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new Dictionary<string, string>();
				}
				return this.attributes;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008588 File Offset: 0x00006788
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000085B5 File Offset: 0x000067B5
		public byte PoisonedCount
		{
			get
			{
				if (this.PoisonedResultCount == null)
				{
					return 0;
				}
				return this.PoisonedResultCount.Value;
			}
			internal set
			{
				this.PoisonedResultCount = new byte?(value);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000207 RID: 519
		// (set) Token: 0x06000208 RID: 520
		public abstract string ExtensionAttributes { get; internal set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000085C3 File Offset: 0x000067C3
		// (set) Token: 0x0600020A RID: 522 RVA: 0x000085CB File Offset: 0x000067CB
		public virtual string InternalStorageKey
		{
			get
			{
				return this.internalStorageKey;
			}
			internal set
			{
				this.internalStorageKey = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000085D4 File Offset: 0x000067D4
		public virtual string ExternalStorageKey
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000085DC File Offset: 0x000067DC
		public virtual string SecondaryExternalStorageKey
		{
			get
			{
				return string.Format("{0}_{1}_{2}_{3}_{4}", new object[]
				{
					Settings.InstanceName,
					Settings.MachineName,
					this.DeploymentSourceName,
					this.TargetVersion,
					this.Id
				});
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600020D RID: 525
		// (set) Token: 0x0600020E RID: 526
		internal abstract int Version { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000862B File Offset: 0x0000682B
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00008633 File Offset: 0x00006833
		internal Type WorkItemType
		{
			get
			{
				return this.workItemType;
			}
			set
			{
				this.workItemType = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000863C File Offset: 0x0000683C
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00008644 File Offset: 0x00006844
		internal object ObjectData
		{
			get
			{
				return this.objectData;
			}
			set
			{
				this.objectData = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000864D File Offset: 0x0000684D
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00008655 File Offset: 0x00006855
		internal DateTime IntendedStartTime { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008660 File Offset: 0x00006860
		internal TracingContext TraceContext
		{
			get
			{
				return new TracingContext(null)
				{
					Id = this.Id,
					LId = this.ExecutionId
				};
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000868D File Offset: 0x0000688D
		internal int ExecutionId
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00008695 File Offset: 0x00006895
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000869D File Offset: 0x0000689D
		protected internal byte? PoisonedResultCount { get; internal set; }

		// Token: 0x06000219 RID: 537 RVA: 0x000086A6 File Offset: 0x000068A6
		public void SetType(Type implementingType)
		{
			this.AssemblyPath = implementingType.Assembly.Location;
			this.TypeName = implementingType.FullName;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000086C5 File Offset: 0x000068C5
		public override string ToString()
		{
			return string.Format("{0}: {1}", base.ToString(), this.Id);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000086E4 File Offset: 0x000068E4
		public virtual void FromXml(XmlNode definition)
		{
			this.AssemblyPath = this.GetMandatoryXmlAttribute<string>(definition, "AssemblyPath");
			this.TypeName = this.GetMandatoryXmlAttribute<string>(definition, "TypeName");
			this.Name = this.GetMandatoryXmlAttribute<string>(definition, "Name");
			this.ServiceName = this.GetMandatoryXmlAttribute<string>(definition, "ServiceName");
			this.RecurrenceIntervalSeconds = this.GetMandatoryXmlAttribute<int>(definition, "RecurrenceIntervalSeconds");
			this.TimeoutSeconds = this.GetMandatoryXmlAttribute<int>(definition, "TimeoutSeconds");
			this.MaxRetryAttempts = this.GetMandatoryXmlAttribute<int>(definition, "MaxRetryAttempts");
			this.Enabled = this.GetMandatoryXmlAttribute<bool>(definition, "Enabled");
			this.DeploymentSourceName = Path.GetFileName(definition.BaseURI);
			XmlNode xmlNode = definition.SelectSingleNode("ExtensionAttributes");
			if (xmlNode != null)
			{
				this.ExtensionAttributes = xmlNode.OuterXml;
				WTFDiagnostics.TraceDebug(WTFLog.DataAccess, this.TraceContext, "[WorkDefinition.FromXml]: Attempting to parse extension attributes.", null, "FromXml", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkDefinition.cs", 431);
				this.ParseExtensionAttributes(false);
				WTFDiagnostics.TraceDebug(WTFLog.DataAccess, this.TraceContext, "[WorkDefinition.FromXml]: Successfully parsed extension attributes.", null, "FromXml", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkDefinition.cs", 435);
			}
			this.TargetPartition = this.GetOptionalXmlAttribute<string>(definition, "TargetPartition", string.Empty);
			this.TargetGroup = this.GetOptionalXmlAttribute<string>(definition, "TargetGroup", string.Empty);
			this.TargetResource = this.GetOptionalXmlAttribute<string>(definition, "TargetResource", string.Empty);
			this.TargetVersion = this.GetOptionalXmlAttribute<string>(definition, "TargetVersion", string.Empty);
			this.TargetExtension = this.GetOptionalXmlAttribute<string>(definition, "TargetExtension", string.Empty);
			this.WorkItemVersion = this.GetOptionalXmlAttribute<string>(definition, "WorkItemVersion", "Binaries");
			this.DeploymentId = this.GetOptionalXmlAttribute<int>(definition, "DeploymentId", 0);
			this.ExecutionLocation = this.GetOptionalXmlAttribute<string>(definition, "ExecutionLocation", string.Empty);
			this.CreatedTime = this.GetOptionalXmlAttribute<DateTime>(definition, "CreatedTime", DateTime.UtcNow);
			this.StartTime = this.GetOptionalXmlAttribute<DateTime>(definition, "StartTime", DateTime.UtcNow);
			this.UpdateTime = this.GetOptionalXmlAttribute<DateTime>(definition, "UpdateTime", DateTime.UtcNow);
			this.LastExecutionStartTime = new DateTime?(this.GetOptionalXmlAttribute<DateTime>(definition, "LastExecutionStartTime", DateTime.UtcNow));
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008914 File Offset: 0x00006B14
		public string ConstructWorkItemResultName()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			if (!string.IsNullOrWhiteSpace(this.TargetPartition))
			{
				stringBuilder.Append("/");
				stringBuilder.Append(this.TargetPartition);
			}
			if (!string.IsNullOrWhiteSpace(this.TargetGroup))
			{
				stringBuilder.Append("/");
				stringBuilder.Append(this.TargetGroup);
			}
			if (!string.IsNullOrWhiteSpace(this.TargetResource))
			{
				stringBuilder.Append("/");
				stringBuilder.Append(this.TargetResource);
			}
			if (!string.IsNullOrWhiteSpace(this.TargetExtension))
			{
				stringBuilder.Append("/");
				stringBuilder.Append(this.TargetExtension);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000089E8 File Offset: 0x00006BE8
		internal static string SerializeExtensionAttributes(IDictionary<string, string> attributes)
		{
			if (attributes != null && attributes.Count > 0)
			{
				return new XElement("ExtensionAttributes", from pair in attributes
				select new XAttribute(pair.Key, pair.Value)).ToString(SaveOptions.DisableFormatting);
			}
			return null;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008A3C File Offset: 0x00006C3C
		internal string GetWorkItemResultTokens(int numberOfTokens)
		{
			IEnumerable<string> values = this.ConstructWorkItemResultName().Split(new string[]
			{
				"/"
			}, StringSplitOptions.RemoveEmptyEntries).Take(numberOfTokens);
			return string.Join("/", values);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008A7C File Offset: 0x00006C7C
		internal ReturnType GetOptionalXmlAttribute<ReturnType>(XmlNode definition, string attributeName, ReturnType defaultValue)
		{
			ReturnType result = default(ReturnType);
			XmlAttribute xmlAttribute = this.GetXmlAttribute(definition, attributeName, false);
			if (xmlAttribute == null)
			{
				WTFDiagnostics.TraceDebug(WTFLog.DataAccess, this.TraceContext, string.Format("Using default value of {0}", defaultValue), null, "GetOptionalXmlAttribute", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkDefinition.cs", 544);
				result = defaultValue;
			}
			else
			{
				result = (ReturnType)((object)Convert.ChangeType(xmlAttribute.Value, typeof(ReturnType)));
			}
			return result;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008AF0 File Offset: 0x00006CF0
		internal ReturnType GetMandatoryXmlAttribute<ReturnType>(XmlNode definition, string attributeName)
		{
			ReturnType returnType = default(ReturnType);
			XmlAttribute xmlAttribute = this.GetXmlAttribute(definition, attributeName, true);
			return (ReturnType)((object)Convert.ChangeType(xmlAttribute.Value, typeof(ReturnType)));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008B2C File Offset: 0x00006D2C
		internal XmlAttribute GetXmlAttribute(XmlNode definition, string attributeName, bool throwOnFailure)
		{
			WTFDiagnostics.TraceDebug<string>(WTFLog.DataAccess, this.TraceContext, "[WorkDefinition.TryGetXmlAttribute]: Attempting to find attribute named {0}.", attributeName, null, "GetXmlAttribute", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkDefinition.cs", 583);
			XmlAttribute xmlAttribute = definition.Attributes[attributeName];
			if (xmlAttribute == null)
			{
				string text = string.Format("Attribute {0} was not found in the WorkDefinition xml.", attributeName);
				XmlAttribute xmlAttribute2 = definition.Attributes["Name"];
				if (xmlAttribute2 != null)
				{
					text = string.Format("{0} WorkDefinition name was {1}", text, xmlAttribute2.Value);
				}
				WTFDiagnostics.TraceDebug(WTFLog.DataAccess, this.TraceContext, text, null, "GetXmlAttribute", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkDefinition.cs", 599);
				if (throwOnFailure)
				{
					throw new XmlException(text);
				}
			}
			return xmlAttribute;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008BD0 File Offset: 0x00006DD0
		internal ReturnType GetMandatoryXmlEnumAttribute<ReturnType>(XmlNode definition, string attributeName)
		{
			XmlAttribute xmlAttribute = this.GetXmlAttribute(definition, attributeName, true);
			return (ReturnType)((object)Enum.Parse(typeof(ReturnType), xmlAttribute.Value));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008C04 File Offset: 0x00006E04
		internal ReturnType GetOptionalXmlEnumAttribute<ReturnType>(XmlNode definition, string attributeName, ReturnType defaultValue)
		{
			XmlAttribute xmlAttribute = this.GetXmlAttribute(definition, attributeName, false);
			ReturnType result = defaultValue;
			if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Value) && Enum.IsDefined(typeof(ReturnType), xmlAttribute.Value))
			{
				result = (ReturnType)((object)Enum.Parse(typeof(ReturnType), xmlAttribute.Value));
			}
			return result;
		}

		// Token: 0x06000224 RID: 548
		internal abstract WorkItemResult CreateResult();

		// Token: 0x06000225 RID: 549 RVA: 0x00008C60 File Offset: 0x00006E60
		internal void ParseExtensionAttributes(bool force = false)
		{
			if ((this.attributes == null || force) && !string.IsNullOrWhiteSpace(this.ExtensionAttributes))
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
				xmlReaderSettings.CloseInput = true;
				xmlReaderSettings.IgnoreComments = true;
				xmlReaderSettings.IgnoreProcessingInstructions = true;
				xmlReaderSettings.IgnoreWhitespace = true;
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(this.ExtensionAttributes), xmlReaderSettings))
				{
					xmlReader.Read();
					this.attributes = new Dictionary<string, string>(xmlReader.AttributeCount);
					for (int i = 0; i < xmlReader.AttributeCount; i++)
					{
						xmlReader.MoveToAttribute(i);
						this.attributes.Add(xmlReader.Name, xmlReader.Value);
					}
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008D2C File Offset: 0x00006F2C
		internal void SyncExtensionAttributes(bool force = false)
		{
			if ((string.IsNullOrWhiteSpace(this.ExtensionAttributes) || force) && this.attributes != null && this.attributes.Count != 0)
			{
				this.ExtensionAttributes = WorkDefinition.SerializeExtensionAttributes(this.attributes);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008D64 File Offset: 0x00006F64
		internal virtual bool Validate(List<string> errors)
		{
			int count = errors.Count;
			if (string.IsNullOrWhiteSpace(this.AssemblyPath))
			{
				errors.Add("AssemblyPath cannot be null or empty. ");
			}
			if (string.IsNullOrWhiteSpace(this.TypeName))
			{
				errors.Add("TypeName cannot be null or empty. ");
			}
			if (string.IsNullOrWhiteSpace(this.Name))
			{
				errors.Add("Name cannot be null or empty. ");
			}
			if (this.RecurrenceIntervalSeconds < 0)
			{
				errors.Add("RecurrenceIntervalSeconds cannot be less than 0. ");
			}
			if (this.TimeoutSeconds <= 0)
			{
				errors.Add("TimeoutSeconds cannot be less than or equal to 0. ");
			}
			if (this.Name.IndexOfAny(this.specialCharacters) != -1)
			{
				errors.Add("Name contains illegal characters. ");
			}
			return count == errors.Count;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008E10 File Offset: 0x00007010
		[Conditional("DEBUG")]
		private void VerifyImplementingType(Type implementingType)
		{
			if (implementingType.IsAbstract)
			{
				throw new ArgumentException("Implementing type cannot be abstract");
			}
			if (implementingType.GetConstructor(new Type[0]) == null)
			{
				throw new ArgumentException("Implementing type should have a default public constructor");
			}
		}

		// Token: 0x040000BF RID: 191
		private const string DefaultDefinitionKey = "DefaultDefinitions";

		// Token: 0x040000C0 RID: 192
		private const string ResultTokenDelimiter = "/";

		// Token: 0x040000C1 RID: 193
		private readonly char[] specialCharacters = new char[]
		{
			'~',
			'\\'
		};

		// Token: 0x040000C2 RID: 194
		private Dictionary<string, string> attributes;

		// Token: 0x040000C3 RID: 195
		private Type workItemType;

		// Token: 0x040000C4 RID: 196
		private object objectData;

		// Token: 0x040000C5 RID: 197
		private string internalStorageKey;
	}
}
