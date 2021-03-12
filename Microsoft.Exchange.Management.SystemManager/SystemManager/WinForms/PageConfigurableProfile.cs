using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000C1 RID: 193
	[Serializable]
	public class PageConfigurableProfile : ITableCentricConfigurable, IHasPermission
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000165F9 File Offset: 0x000147F9
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00016601 File Offset: 0x00014801
		[DDIMandatoryValue]
		public string Name { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0001660A File Offset: 0x0001480A
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00016614 File Offset: 0x00014814
		public Type[] LambdaExpressionHelper
		{
			get
			{
				return this.lambdaExpressionHelper;
			}
			set
			{
				if (value != this.lambdaExpressionHelper)
				{
					this.lambdaExpressionHelper = value;
					if (this.LambdaExpressionHelper != null)
					{
						foreach (Type type in this.LambdaExpressionHelper)
						{
							ExpressionParser.EnrolPredefinedTypes(type);
						}
					}
				}
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00016658 File Offset: 0x00014858
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00016660 File Offset: 0x00014860
		public DataObjectProfileList DataObjectProfiles
		{
			get
			{
				return this.dataObjectProfiles;
			}
			set
			{
				this.dataObjectProfiles = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00016669 File Offset: 0x00014869
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00016671 File Offset: 0x00014871
		public ReaderTaskProfileList ReaderTaskProfiles
		{
			get
			{
				return this.readerTaskProfileList;
			}
			set
			{
				this.readerTaskProfileList = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001667A File Offset: 0x0001487A
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00016682 File Offset: 0x00014882
		public SaverTaskProfileList SaverTaskProfiles
		{
			get
			{
				return this.saverTaskProfiles;
			}
			set
			{
				this.saverTaskProfiles = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001668B File Offset: 0x0001488B
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x00016693 File Offset: 0x00014893
		[DDIMandatoryValue]
		public ColumnProfileList ColumnProfiles
		{
			get
			{
				return this.columnProfiles;
			}
			set
			{
				this.columnProfiles = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001669C File Offset: 0x0001489C
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x000166A4 File Offset: 0x000148A4
		public PageToDataObjectsList PageToDataObjectsMapping
		{
			get
			{
				return this.pageToDataObjectsMapping;
			}
			set
			{
				this.pageToDataObjectsMapping = value;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000166AD File Offset: 0x000148AD
		public List<ReaderTaskProfile> BuildReaderTaskProfile()
		{
			return this.ReaderTaskProfiles;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000166B5 File Offset: 0x000148B5
		public List<SaverTaskProfile> BuildSaverTaskProfile()
		{
			return this.SaverTaskProfiles;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000166BD File Offset: 0x000148BD
		public List<DataObjectProfile> BuildDataObjectProfile()
		{
			return this.DataObjectProfiles;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000166C5 File Offset: 0x000148C5
		public List<ColumnProfile> BuildColumnProfile()
		{
			return this.ColumnProfiles;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000166D0 File Offset: 0x000148D0
		public Dictionary<string, List<string>> BuildPageToDataObjectsMapping()
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			foreach (string key in this.PageToDataObjectsMapping.Keys)
			{
				dictionary[key] = this.PageToDataObjectsMapping[key].ToList<string>();
			}
			return dictionary;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00016740 File Offset: 0x00014940
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00016748 File Offset: 0x00014948
		[DefaultValue(true)]
		public bool EnableUICustomization
		{
			get
			{
				return this.enableUICustomization;
			}
			set
			{
				this.enableUICustomization = value;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00016751 File Offset: 0x00014951
		public bool CanEnableUICustomization()
		{
			return this.EnableUICustomization;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001675C File Offset: 0x0001495C
		public bool HasPermission()
		{
			bool result = false;
			foreach (ReaderTaskProfile readerTaskProfile in this.readerTaskProfileList)
			{
				if (readerTaskProfile.HasPermission())
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x04000206 RID: 518
		private Type[] lambdaExpressionHelper;

		// Token: 0x04000207 RID: 519
		private DataObjectProfileList dataObjectProfiles = new DataObjectProfileList();

		// Token: 0x04000208 RID: 520
		private ReaderTaskProfileList readerTaskProfileList = new ReaderTaskProfileList();

		// Token: 0x04000209 RID: 521
		private SaverTaskProfileList saverTaskProfiles = new SaverTaskProfileList();

		// Token: 0x0400020A RID: 522
		private ColumnProfileList columnProfiles = new ColumnProfileList();

		// Token: 0x0400020B RID: 523
		private PageToDataObjectsList pageToDataObjectsMapping = new PageToDataObjectsList();

		// Token: 0x0400020C RID: 524
		private bool enableUICustomization = true;
	}
}
