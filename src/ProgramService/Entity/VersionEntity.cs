using System;
using System.ComponentModel.DataAnnotations;

namespace ProgramService.Entity
{
	public class VersionEntity
	{
		public Entity.ProgramEntity Program { get; set; } = new ProgramEntity();
		[Key]
		public string VersionTag { get; set; } = string.Empty;
		public string Uri { get; set; } = string.Empty;
	}
}

