using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Xml;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000024 RID: 36
	[Table]
	public sealed class ProbeDefinition : WorkDefinition, IPersistence
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000B4B3 File Offset: 0x000096B3
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000B4BB File Offset: 0x000096BB
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public override int Id { get; internal set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000B4C4 File Offset: 0x000096C4
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000B4CC File Offset: 0x000096CC
		[Column]
		public override string AssemblyPath { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000B4D5 File Offset: 0x000096D5
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000B4DD File Offset: 0x000096DD
		[Column]
		public override string TypeName { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000B4E6 File Offset: 0x000096E6
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000B4EE File Offset: 0x000096EE
		[Column]
		public override string Name { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000B4F7 File Offset: 0x000096F7
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000B4FF File Offset: 0x000096FF
		[Column]
		public override string WorkItemVersion { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000B508 File Offset: 0x00009708
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000B510 File Offset: 0x00009710
		[Column]
		public override string ServiceName { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000B519 File Offset: 0x00009719
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000B521 File Offset: 0x00009721
		[Column]
		public override int DeploymentId { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B52A File Offset: 0x0000972A
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000B532 File Offset: 0x00009732
		[Column]
		public override string ExecutionLocation { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B53B File Offset: 0x0000973B
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000B543 File Offset: 0x00009743
		[Column]
		public override DateTime CreatedTime { get; internal set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000B54C File Offset: 0x0000974C
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000B554 File Offset: 0x00009754
		[Column]
		public override bool Enabled { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000B55D File Offset: 0x0000975D
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000B565 File Offset: 0x00009765
		[Column]
		public override string TargetPartition { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000B56E File Offset: 0x0000976E
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000B576 File Offset: 0x00009776
		[Column]
		public override string TargetGroup { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000B57F File Offset: 0x0000977F
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000B587 File Offset: 0x00009787
		[Column]
		public override string TargetResource { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000B590 File Offset: 0x00009790
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000B598 File Offset: 0x00009798
		[Column]
		public override string TargetExtension { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000B5A1 File Offset: 0x000097A1
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000B5A9 File Offset: 0x000097A9
		[Column]
		public override string TargetVersion { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000B5B2 File Offset: 0x000097B2
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000B5BA File Offset: 0x000097BA
		[Column]
		public override int RecurrenceIntervalSeconds { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000B5C3 File Offset: 0x000097C3
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000B5CB File Offset: 0x000097CB
		[Column]
		public override int TimeoutSeconds { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000B5D4 File Offset: 0x000097D4
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000B5DC File Offset: 0x000097DC
		[Column]
		public override DateTime StartTime { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000B5E5 File Offset: 0x000097E5
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000B5ED File Offset: 0x000097ED
		[Column]
		public override DateTime UpdateTime { get; internal set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000B5F6 File Offset: 0x000097F6
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000B5FE File Offset: 0x000097FE
		[Column]
		public override int MaxRetryAttempts { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000B607 File Offset: 0x00009807
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000B60F File Offset: 0x0000980F
		[Column]
		public override string ExtensionAttributes { get; internal set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B618 File Offset: 0x00009818
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000B620 File Offset: 0x00009820
		[Column]
		public override int CreatedById { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000B629 File Offset: 0x00009829
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000B631 File Offset: 0x00009831
		[Column]
		[PropertyInformation("The email address identifying the primary account.", false)]
		public string Account { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B63A File Offset: 0x0000983A
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000B642 File Offset: 0x00009842
		[Column]
		[PropertyInformation("The password of the primary account.", false)]
		public string AccountPassword { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B64B File Offset: 0x0000984B
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000B653 File Offset: 0x00009853
		[Column]
		[PropertyInformation("The display name of the primary account.", false)]
		public string AccountDisplayName { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B65C File Offset: 0x0000985C
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000B664 File Offset: 0x00009864
		[Column]
		[PropertyInformation("The primary protocol URL used by the probe.", false)]
		public string Endpoint { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B66D File Offset: 0x0000986D
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000B675 File Offset: 0x00009875
		[Column]
		[PropertyInformation("The email address identifying the secondary account.", false)]
		public string SecondaryAccount { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B67E File Offset: 0x0000987E
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000B686 File Offset: 0x00009886
		[PropertyInformation("The password of the secondary account.", false)]
		[Column]
		public string SecondaryAccountPassword { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000B68F File Offset: 0x0000988F
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000B697 File Offset: 0x00009897
		[Column]
		[PropertyInformation("The display name of the secondary account.", false)]
		public string SecondaryAccountDisplayName { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000B6A0 File Offset: 0x000098A0
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000B6A8 File Offset: 0x000098A8
		[Column]
		[PropertyInformation("The secondary protocol URL used by the probe.", false)]
		public string SecondaryEndpoint { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000B6B1 File Offset: 0x000098B1
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000B6B9 File Offset: 0x000098B9
		[PropertyInformation("The extension endpoints or vips to be used by the probe.", false)]
		[Column]
		public string ExtensionEndpoints { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B6C2 File Offset: 0x000098C2
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000B6CA File Offset: 0x000098CA
		[Column]
		internal override int Version { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000B6D3 File Offset: 0x000098D3
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000B6DB File Offset: 0x000098DB
		[Column]
		internal int ExecutionType { get; set; }

		// Token: 0x060002AA RID: 682 RVA: 0x0000B6E4 File Offset: 0x000098E4
		public override void FromXml(XmlNode definition)
		{
			base.FromXml(definition);
			this.Account = base.GetOptionalXmlAttribute<string>(definition, "Account", string.Empty);
			this.AccountPassword = base.GetOptionalXmlAttribute<string>(definition, "AccountPassword", string.Empty);
			this.AccountDisplayName = base.GetOptionalXmlAttribute<string>(definition, "AccountDisplayName", string.Empty);
			this.Endpoint = base.GetOptionalXmlAttribute<string>(definition, "Endpoint", string.Empty);
			this.SecondaryAccount = base.GetOptionalXmlAttribute<string>(definition, "SecondaryAccount", string.Empty);
			this.SecondaryAccountPassword = base.GetOptionalXmlAttribute<string>(definition, "SecondaryAccountPassword", string.Empty);
			this.SecondaryAccountDisplayName = base.GetOptionalXmlAttribute<string>(definition, "SecondaryAccountDisplayName", string.Empty);
			this.SecondaryEndpoint = base.GetOptionalXmlAttribute<string>(definition, "SecondaryEndpoint", string.Empty);
			this.ExecutionType = base.GetOptionalXmlAttribute<int>(definition, "ExecutionType", 0);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B7C3 File Offset: 0x000099C3
		internal override WorkItemResult CreateResult()
		{
			return new ProbeResult(this);
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000B7CB File Offset: 0x000099CB
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000B7D3 File Offset: 0x000099D3
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000B7DC File Offset: 0x000099DC
		internal static int SchemaVersion
		{
			get
			{
				return ProbeDefinition.schemaVersion;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B7E3 File Offset: 0x000099E3
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000B7EA File Offset: 0x000099EA
		internal static IEnumerable<WorkDefinitionOverride> GlobalOverrides { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B7F2 File Offset: 0x000099F2
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000B7F9 File Offset: 0x000099F9
		internal static IEnumerable<WorkDefinitionOverride> LocalOverrides { get; set; }

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B801 File Offset: 0x00009A01
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B814 File Offset: 0x00009A14
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			string text;
			if (propertyBag.TryGetValue("Id", out text) && !string.IsNullOrEmpty(text))
			{
				this.Id = int.Parse(text);
			}
			if (propertyBag.TryGetValue("AssemblyPath", out text))
			{
				this.AssemblyPath = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TypeName", out text))
			{
				this.TypeName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Name", out text))
			{
				this.Name = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("WorkItemVersion", out text))
			{
				this.WorkItemVersion = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ServiceName", out text))
			{
				this.ServiceName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("DeploymentId", out text) && !string.IsNullOrEmpty(text))
			{
				this.DeploymentId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExecutionLocation", out text))
			{
				this.ExecutionLocation = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("CreatedTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.CreatedTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("Enabled", out text) && !string.IsNullOrEmpty(text))
			{
				this.Enabled = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("TargetPartition", out text))
			{
				this.TargetPartition = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetGroup", out text))
			{
				this.TargetGroup = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetResource", out text))
			{
				this.TargetResource = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetExtension", out text))
			{
				this.TargetExtension = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetVersion", out text))
			{
				this.TargetVersion = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("RecurrenceIntervalSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.RecurrenceIntervalSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("TimeoutSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.TimeoutSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("StartTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.StartTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("UpdateTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.UpdateTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("MaxRetryAttempts", out text) && !string.IsNullOrEmpty(text))
			{
				this.MaxRetryAttempts = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExtensionAttributes", out text))
			{
				this.ExtensionAttributes = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("CreatedById", out text) && !string.IsNullOrEmpty(text))
			{
				this.CreatedById = int.Parse(text);
			}
			if (propertyBag.TryGetValue("Account", out text))
			{
				this.Account = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("AccountDisplayName", out text))
			{
				this.AccountDisplayName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Endpoint", out text))
			{
				this.Endpoint = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("SecondaryAccount", out text))
			{
				this.SecondaryAccount = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("SecondaryAccountDisplayName", out text))
			{
				this.SecondaryAccountDisplayName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("SecondaryEndpoint", out text))
			{
				this.SecondaryEndpoint = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ExtensionEndpoints", out text))
			{
				this.ExtensionEndpoints = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Version", out text) && !string.IsNullOrEmpty(text))
			{
				this.Version = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExecutionType", out text) && !string.IsNullOrEmpty(text))
			{
				this.ExecutionType = int.Parse(text);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			DefinitionIdGenerator<ProbeDefinition>.AssignId(this);
			Update<ProbeDefinition>.ApplyUpdates(this);
			if (ProbeDefinition.GlobalOverrides != null)
			{
				foreach (WorkDefinitionOverride definitionOverride in ProbeDefinition.GlobalOverrides)
				{
					definitionOverride.TryApplyTo(this, base.TraceContext);
				}
			}
			if (ProbeDefinition.LocalOverrides != null)
			{
				foreach (WorkDefinitionOverride definitionOverride2 in ProbeDefinition.LocalOverrides)
				{
					definitionOverride2.TryApplyTo(this, base.TraceContext);
				}
			}
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			NativeMethods.ProbeDefinitionUnmanaged probeDefinitionUnmanaged = this.ToUnmanaged();
			NativeMethods.WriteProbeDefinition(ref probeDefinitionUnmanaged);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000BCAC File Offset: 0x00009EAC
		internal NativeMethods.ProbeDefinitionUnmanaged ToUnmanaged()
		{
			return new NativeMethods.ProbeDefinitionUnmanaged
			{
				Id = this.Id,
				AssemblyPath = CrimsonHelper.NullCode(this.AssemblyPath),
				TypeName = CrimsonHelper.NullCode(this.TypeName),
				Name = CrimsonHelper.NullCode(this.Name),
				WorkItemVersion = CrimsonHelper.NullCode(this.WorkItemVersion),
				ServiceName = CrimsonHelper.NullCode(this.ServiceName),
				DeploymentId = this.DeploymentId,
				ExecutionLocation = CrimsonHelper.NullCode(this.ExecutionLocation),
				CreatedTime = this.CreatedTime.ToUniversalTime().ToString("o"),
				Enabled = (this.Enabled ? 1 : 0),
				TargetPartition = CrimsonHelper.NullCode(this.TargetPartition),
				TargetGroup = CrimsonHelper.NullCode(this.TargetGroup),
				TargetResource = CrimsonHelper.NullCode(this.TargetResource),
				TargetExtension = CrimsonHelper.NullCode(this.TargetExtension),
				TargetVersion = CrimsonHelper.NullCode(this.TargetVersion),
				RecurrenceIntervalSeconds = this.RecurrenceIntervalSeconds,
				TimeoutSeconds = this.TimeoutSeconds,
				StartTime = this.StartTime.ToUniversalTime().ToString("o"),
				UpdateTime = this.UpdateTime.ToUniversalTime().ToString("o"),
				MaxRetryAttempts = this.MaxRetryAttempts,
				ExtensionAttributes = CrimsonHelper.NullCode(this.ExtensionAttributes),
				CreatedById = this.CreatedById,
				Account = CrimsonHelper.NullCode(this.Account),
				AccountDisplayName = CrimsonHelper.NullCode(this.AccountDisplayName),
				Endpoint = CrimsonHelper.NullCode(this.Endpoint),
				SecondaryAccount = CrimsonHelper.NullCode(this.SecondaryAccount),
				SecondaryAccountDisplayName = CrimsonHelper.NullCode(this.SecondaryAccountDisplayName),
				SecondaryEndpoint = CrimsonHelper.NullCode(this.SecondaryEndpoint),
				ExtensionEndpoints = CrimsonHelper.NullCode(this.ExtensionEndpoints),
				Version = this.Version,
				ExecutionType = this.ExecutionType
			};
		}

		// Token: 0x0400024C RID: 588
		private static int schemaVersion = 65536;
	}
}
