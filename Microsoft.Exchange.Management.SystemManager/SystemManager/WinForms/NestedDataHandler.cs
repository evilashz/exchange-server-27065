using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200014B RID: 331
	public class NestedDataHandler : DataHandler
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x000322AA File Offset: 0x000304AA
		public NestedDataHandler(DataContext parentContext)
		{
			this.parentContext = parentContext;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000322BC File Offset: 0x000304BC
		internal override void OnReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			ICloneable cloneable = (ICloneable)this.parentContext.ReadData(interactionHandler, pageName);
			int count = this.parentContext.DataHandler.DataHandlers.Count;
			if (count != 0)
			{
				this.dataSources = new object[count];
				for (int i = 0; i < count; i++)
				{
					DataHandler dataHandler = this.parentContext.DataHandler.DataHandlers[i];
					if (dataHandler.ReadOnly)
					{
						this.dataSources[i] = dataHandler.DataSource;
					}
					else if (dataHandler.DataSource is ICloneable)
					{
						this.dataSources[i] = ((ICloneable)dataHandler.DataSource).Clone();
					}
					else
					{
						this.dataSources[i] = null;
					}
				}
				base.DataSource = this.DataSources[0];
				return;
			}
			this.dataSources = new object[0];
			base.DataSource = (this.parentContext.DataHandler.ReadOnly ? cloneable : cloneable.Clone());
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000323AC File Offset: 0x000305AC
		internal override void SpecifyParameterNames(Dictionary<object, List<string>> bindingMembers)
		{
			Dictionary<object, List<string>> dictionary = new Dictionary<object, List<string>>();
			if (this.DataSources.Length > 0)
			{
				for (int i = 0; i < this.DataSources.Length; i++)
				{
					if (this.DataSources[i] != null && bindingMembers.ContainsKey(this.DataSources[i]))
					{
						dictionary.Add(this.parentContext.DataHandler.DataHandlers[i].DataSource, bindingMembers[this.DataSources[i]]);
					}
				}
			}
			else if (base.DataSource != null && !this.parentContext.DataHandler.ReadOnly && bindingMembers.ContainsKey(base.DataSource))
			{
				dictionary.Add(this.parentContext.DataHandler.DataSource, bindingMembers[base.DataSource]);
			}
			this.parentContext.DataHandler.SpecifyParameterNames(dictionary);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00032484 File Offset: 0x00030684
		internal override void OnSaveData(CommandInteractionHandler interactionHandler)
		{
			if (this.parentContext.DataHandler.DataHandlers.Count != 0)
			{
				for (int i = 0; i < this.DataSources.Length; i++)
				{
					DataHandler dataHandler = this.parentContext.DataHandler.DataHandlers[i];
					if (this.DataSources[i] != null && !dataHandler.ReadOnly)
					{
						if (i == 0)
						{
							this.parentContext.DataHandler.DataSource = this.DataSources[i];
						}
						dataHandler.DataSource = this.DataSources[i];
					}
				}
			}
			else
			{
				this.parentContext.DataHandler.DataSource = base.DataSource;
			}
			this.parentContext.IsDirty = true;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00032531 File Offset: 0x00030731
		public object[] DataSources
		{
			get
			{
				return this.dataSources;
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003253C File Offset: 0x0003073C
		public override ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			if (this.DataSources.Length > 0)
			{
				foreach (object obj in this.DataSources)
				{
					if (obj is IConfigurable)
					{
						ValidationError[] array2 = (obj as IConfigurable).Validate();
						if (array2 != null)
						{
							list.AddRange(array2);
						}
					}
				}
			}
			else if (base.DataSource is IConfigurable)
			{
				ValidationError[] array2 = (base.DataSource as IConfigurable).Validate();
				if (array2 != null)
				{
					list.AddRange(array2);
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000325C6 File Offset: 0x000307C6
		internal override bool TimeConsuming
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400055B RID: 1371
		private DataContext parentContext;

		// Token: 0x0400055C RID: 1372
		private object[] dataSources;
	}
}
