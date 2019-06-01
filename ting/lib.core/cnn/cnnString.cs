namespace ting.lib
{
    /// <summary>
    /// cnnstring object
    /// </summary>
    public class cnnString
    {
        /// <summary>
        /// data source
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// initial catalog
        /// </summary>
        public string InitialCatalog { get; set; }
        /// <summary>
        /// integrated security
        /// </summary>
        public string IntegratedSecurity { get; set; }
        /// <summary>
        /// user id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// convert cnnstring string to cnnstring object
        /// </summary>
        public string ConvertcnnString(cnnString cnn)
        {
            string result = null;
            if (cnn.IntegratedSecurity == null)
            {
                result = string.Format(
                    "Data Source = {0}; Initial Catalog = {1}; User Id = {2}; Password = {3}",
                    cnn.DataSource, cnn.InitialCatalog, cnn.UserId, cnn.Password);
            }
            else if (cnn.IntegratedSecurity.Trim().ToUpper() == "TRUE")
            {
                result = string.Format(
                    "Data Source = {0}; Initial Catalog = {1}; Integrated Security = {2}",
                    cnn.DataSource, cnn.InitialCatalog, cnn.IntegratedSecurity);
            }
            else if (cnn.IntegratedSecurity.Trim().ToUpper() == "FALSE")
            {
                result = string.Format(
                    "Data Source = {0}; Initial Catalog = {1}; User Id = {2}; Password = {3}",
                    cnn.DataSource, cnn.InitialCatalog, cnn.UserId, cnn.Password);
            }
            return result;
        }
        /// <summary>
        /// convert cnnstring object to cnnstring string
        /// </summary>
        public cnnString ConvertcnnString(string cnn)
        {
            cnnString result = new cnnString();
            if (cnn != null)
            {
                var cp = cnn.Split(';');
                foreach (var item in cp)
                {
                    var p = item.Split('=');
                    switch (p[0].ToUpper().Trim())
                    {
                        case "DATA SOURCE":
                            result.DataSource = p[1];
                            break;
                        case "INITIAL CATALOG":
                            result.InitialCatalog = p[1];
                            break;
                        case "INTEGRATED SECURITY":
                            result.IntegratedSecurity = p[1];
                            break;
                        case "USER ID":
                            result.UserId = p[1];
                            break;
                        case "PASSWORD":
                            result.Password = p[1];
                            break;
                        default:
                            break;
                    }
                }
            }
            else { result = null; }
            return result;
        }
    }

}
