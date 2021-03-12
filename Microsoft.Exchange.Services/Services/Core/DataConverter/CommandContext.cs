using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F3 RID: 243
	internal sealed class CommandContext
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00022978 File Offset: 0x00020B78
		public CommandContext(CommandSettings commandSettings, PropertyInformation propertyInformation)
		{
			this.commandSettings = commandSettings;
			this.propertyInformation = propertyInformation;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0002298E File Offset: 0x00020B8E
		public CommandContext(CommandSettings commandSettings, PropertyInformation propertyInformation, IdConverter idConverter) : this(commandSettings, propertyInformation)
		{
			this.idConverter = idConverter;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0002299F File Offset: 0x00020B9F
		public PropertyDefinition[] GetPropertyDefinitions()
		{
			if (this.propertyDefinitions == null)
			{
				this.propertyDefinitions = this.propertyInformation.GetPropertyDefinitions(this.commandSettings);
			}
			return this.propertyDefinitions;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000229C6 File Offset: 0x00020BC6
		public CommandSettings CommandSettings
		{
			get
			{
				return this.commandSettings;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x000229CE File Offset: 0x00020BCE
		public PropertyInformation PropertyInformation
		{
			get
			{
				return this.propertyInformation;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000229D6 File Offset: 0x00020BD6
		public IdConverter IdConverter
		{
			get
			{
				return this.idConverter;
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000229E0 File Offset: 0x00020BE0
		public override string ToString()
		{
			if (this.propertyInformation == null)
			{
				return base.GetType().Name;
			}
			if (this.propertyInformation.PropertyPath == null)
			{
				return base.GetType().Name;
			}
			PropertyUri propertyUri = this.propertyInformation.PropertyPath as PropertyUri;
			if (propertyUri != null)
			{
				return propertyUri.UriString;
			}
			if (this.GetPropertyDefinitions().Length > 0)
			{
				return this.GetPropertyDefinitions()[0].ToString();
			}
			return base.GetType().Name;
		}

		// Token: 0x040006CA RID: 1738
		private CommandSettings commandSettings;

		// Token: 0x040006CB RID: 1739
		private PropertyInformation propertyInformation;

		// Token: 0x040006CC RID: 1740
		private PropertyDefinition[] propertyDefinitions;

		// Token: 0x040006CD RID: 1741
		private IdConverter idConverter;
	}
}
