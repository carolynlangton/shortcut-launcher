namespace ShortcutLauncher
{
    /// <summary>
    /// The class that represents a shortcut.
    /// </summary>
    public abstract class Shortcut
    {
        /// <summary>
        /// Gets or sets the shortcut's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the shortcut's path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the shortcut's type.
        /// </summary>
        public ShortcutType Type { get; set; }

        /// <summary>
        /// Creates a string representation of a shortcut.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return this.Description;
        }
    }
}