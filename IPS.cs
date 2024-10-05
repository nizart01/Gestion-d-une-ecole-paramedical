using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace IPS_SANTE
{
    public class IPS
    {
        public static string Role = "";

        public static SqlConnection GetConnection(string sql)
        {
            SqlConnection connection = new SqlConnection(sql);
            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "SQL connection", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return connection;
        }

        public static string IsValidNamePass(string Username, string Password, string connectionString)
        {
            try
            {
                string cmdText = "SELECT User_Name, User_Pass, User_Role FROM User_Table WHERE User_Name = '" + Username + "' AND User_Pass = '" + Password + "'";
                SqlConnection connection = IPS.GetConnection(connectionString);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(cmdText, connection));
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                connection.Close();
                if (dataTable.Rows.Count > 0)
                    IPS.Role = dataTable.Rows[0][2].ToString();
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Username and Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return IPS.Role;
        }

        public static bool AddRole(
          string Name,
          string Password,
          string Phone,
          string CNIC,
          string DOB,
          string Gender,
          string Email,
          string Role,
          string Address,
          string connectionString)
        {
            string cmdText = "INSERT INTO User_Table VALUES (@User_Name, @User_Pass, @User_Pho, @User_CNIC, @User_DOB, @User_Gender, @User_Email, @User_Role, @User_Add)";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = (object)Name;
            sqlCommand.Parameters.Add("@User_Pass", SqlDbType.VarChar).Value = (object)Password;
            sqlCommand.Parameters.Add("@User_Pho", SqlDbType.VarChar).Value = (object)Phone;
            sqlCommand.Parameters.Add("@User_CNIC", SqlDbType.VarChar).Value = (object)CNIC;
            sqlCommand.Parameters.Add("@User_DOB", SqlDbType.VarChar).Value = (object)DOB;
            sqlCommand.Parameters.Add("@User_Gender", SqlDbType.VarChar).Value = (object)Gender;
            sqlCommand.Parameters.Add("@User_Email", SqlDbType.VarChar).Value = (object)Email;
            sqlCommand.Parameters.Add("@User_Role", SqlDbType.VarChar).Value = (object)Role;
            sqlCommand.Parameters.Add("@User_Add", SqlDbType.VarChar).Value = (object)Address;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Ajouter Avec Succes!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    int num1 = (int)MessageBox.Show("Utilisateur ou CNI existe deja.", "Username or CNIC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    int num2 = (int)MessageBox.Show("Error! \n" + ex.ToString(), "User add", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                return false;
            }
            connection.Close();
            return true;
        }

        public static bool UpdateUser(
          string ID,
          string Name,
          string Password,
          string Phone,
          string CNIC,
          string DOB,
          string Gender,
          string Email,
          string Role,
          string Address,
          string connectionString)
        {
            string cmdText = "UPDATE User_Table SET User_Name = @UserName, User_Pass = @UserPass, User_Pho = @UserPho, User_CNIC = @UserCNIC, User_DOB = @UserDOB, User_Gender = @UserGender, User_Email = @UserEmail, User_Role = @UserRole, User_Add = @UserAdd WHERE User_ID = @UserID;";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = (object)ID;
            sqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = (object)Name;
            sqlCommand.Parameters.Add("@UserPass", SqlDbType.VarChar).Value = (object)Password;
            sqlCommand.Parameters.Add("@UserPho", SqlDbType.VarChar).Value = (object)Phone;
            sqlCommand.Parameters.Add("@UserCNIC", SqlDbType.VarChar).Value = (object)CNIC;
            sqlCommand.Parameters.Add("@UserDOB", SqlDbType.VarChar).Value = (object)DOB;
            sqlCommand.Parameters.Add("@UserGender", SqlDbType.VarChar).Value = (object)Gender;
            sqlCommand.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = (object)Email;
            sqlCommand.Parameters.Add("@UserRole", SqlDbType.VarChar).Value = (object)Role;
            sqlCommand.Parameters.Add("@UserAdd", SqlDbType.VarChar).Value = (object)Address;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Modification effectue!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    int num1 = (int)MessageBox.Show("Utilisateur ou CNI existe deja.", "Username or CNIC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    int num2 = (int)MessageBox.Show("Error! \n" + ex.ToString(), "User update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                return false;
            }
            connection.Close();
            return true;
        }

        public static bool DeleteUser(string ID, string connectionString)
        {
            string cmdText = "DELETE FROM User_Table WHERE User_ID = @UserID";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = (object)ID;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Suppression Effectue !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "User delete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            connection.Close();
            return true;
        }

        public static bool AddClass(
          string Name,
          string Total,
          string Male,
          string Female,
          string connectionString)
        {
            string cmdText = "INSERT INTO Class_Table VALUES (@Class_Name, @Class_Total, @Class_Male, @Class_Female)";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@Class_Name", SqlDbType.VarChar).Value = (object)Name;
            sqlCommand.Parameters.Add("@Class_Total", SqlDbType.VarChar).Value = (object)Total;
            sqlCommand.Parameters.Add("@Class_Male", SqlDbType.VarChar).Value = (object)Male;
            sqlCommand.Parameters.Add("@Class_Female", SqlDbType.VarChar).Value = (object)Female;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Class add", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            connection.Close();
            return true;
        }

        public static void DisplayAndSearchAllData(
          string query,
          DataGridView dgv,
          string connectionString)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(query, IPS.GetConnection(connectionString)));
            DataTable dataTable = new DataTable();
             sqlDataAdapter.Fill(dataTable);
            dgv.DataSource = (object)dataTable;
        }


        public static bool UpdateClass(
          string ID,
          string Name,
          string Total,
          string Male,
          string Female,
          string connectionString)
        {
            string cmdText = "UPDATE Class_Table SET Class_Name = @ClassName, Class_Total = @ClassTotal, Class_Male = @ClassMale, Class_Female = @ClassFemale WHERE Class_ID = @ClassID";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@ClassID", SqlDbType.Int).Value = (object)ID;
            sqlCommand.Parameters.Add("@ClassName", SqlDbType.VarChar).Value = (object)Name;
            sqlCommand.Parameters.Add("@ClassTotal", SqlDbType.VarChar).Value = (object)Total;
            sqlCommand.Parameters.Add("@ClassMale", SqlDbType.VarChar).Value = (object)Male;
            sqlCommand.Parameters.Add("@ClassFemale", SqlDbType.VarChar).Value = (object)Female;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Modification Effectue !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Class update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            connection.Close();
            return true;
        }

        public static bool DeleteClass(string CID, string connectionString)
        {
            string cmdText = "DELETE FROM Class_Table WHERE Class_ID = @ClassID";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@ClassID", SqlDbType.Int).Value = (object)CID;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Suppression Effectuer!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Class delete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            connection.Close();
            return true;
        }

        public static void FillComboBox(string query, ComboBox cb, string connectionString)
        {
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            try
            {
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    cb.Items.Add(sqlDataReader[0]);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Class not present.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            connection.Close();
        }

        public static int GetId(string query, string connectionString)
        {
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            try
            {
                return Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Class id not find.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            connection.Close();
            return 0;
        }

        public static bool AddStudent(
    string ClassName,
    string Nom,
    string Prenom,
    string CIN,
    string Massar,
    string RegN,
    string RegD,
    string DN,
    string LN,
    string Pho,
    string PhoT,
    string DE,
    string Email,
    string ND,
    string SN,
    string adress,
    string NS,
    byte[] PictureData, 
    string connectionString)
        {
            string cmdText = "INSERT INTO Student_Table (Class_ID, Student_Name, Student_Prenom, Student_CIN, Student_Massar, Student_No, Student_DateI, Student_DateN, Student_LieuN, Student_Phone, Student_PhoneT, Student_DernierE, Student_Email, Student_NiveauD, Student_SerieNo, Student_Address, Student_NiveauS, Student_Picture) VALUES (@Class_ID, @Student_Name, @Student_Prenom, @Student_CIN, @Student_Massar, @Student_No, @Student_DateI, @Student_DateN, @Student_LieuN, @Student_Phone, @Student_PhoneT, @Student_DernierE, @Student_Email, @Student_NiveauD, @Student_SerieNo, @Student_Address, @Student_NiveauS, @Student_Picture)"; 
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@Class_ID", SqlDbType.Int).Value = (object)IPS.GetId("SELECT Class_ID FROM Class_Table WHERE Class_Name = '" + ClassName + "';", connectionString);
            sqlCommand.Parameters.Add("@Student_Name", SqlDbType.VarChar).Value = (object)Nom;
            sqlCommand.Parameters.Add("@Student_Prenom", SqlDbType.VarChar).Value = (object)Prenom;
            sqlCommand.Parameters.Add("@Student_CIN", SqlDbType.VarChar).Value = (object)CIN;
            sqlCommand.Parameters.Add("@Student_Massar", SqlDbType.VarChar).Value = (object)Massar;
            sqlCommand.Parameters.Add("@Student_No", SqlDbType.VarChar).Value = (object)RegN;
            sqlCommand.Parameters.Add("@Student_DateI", SqlDbType.VarChar).Value = (object)RegD;
            sqlCommand.Parameters.Add("@Student_DateN", SqlDbType.VarChar).Value = (object)DN;
            sqlCommand.Parameters.Add("@Student_LieuN", SqlDbType.VarChar).Value = (object)LN;
            sqlCommand.Parameters.Add("@Student_Phone", SqlDbType.VarChar).Value = (object)Pho;
            sqlCommand.Parameters.Add("@Student_PhoneT", SqlDbType.VarChar).Value = (object)PhoT;
            sqlCommand.Parameters.Add("@Student_DernierE", SqlDbType.VarChar).Value = (object)DE;
            sqlCommand.Parameters.Add("@Student_Email", SqlDbType.VarChar).Value = (object)Email;
            sqlCommand.Parameters.Add("@Student_NiveauD", SqlDbType.VarChar).Value = (object)ND;
            sqlCommand.Parameters.Add("@Student_SerieNo", SqlDbType.VarChar).Value = (object)SN;
            sqlCommand.Parameters.Add("@Student_Address", SqlDbType.VarChar).Value = (object)adress;
            sqlCommand.Parameters.Add("@Student_NiveauS", SqlDbType.VarChar).Value = (object)NS;
            sqlCommand.Parameters.Add("@Student_Picture", SqlDbType.VarBinary).Value = (object)PictureData;

            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Ajouter Avec Succes!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Student add", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            connection.Close();
            return true;
        }


    public static bool UpdateStudent(
         string ID,
                  string ClassName,
         string Nom,
         string Prenom,
         string CIN,
         string Massar,
         string RegN,
         string RegD,
         string DN,
         string LN,
         string Pho,
         string PhoT,
         string DE,
         string Email,
         string ND,
         string SN,
         string adress,
         string NS,
         byte[] PictureData,
         string connectionString)
            {
                try
                {
                    string cmdText = "UPDATE Student_Table SET  Class_ID = @Class_ID, Student_Name = @Student_Name, Student_Prenom = @Student_Prenom, Student_CIN = @Student_CIN, Student_Massar = @Student_Massar, Student_No = @Student_No, Student_DateI = @Student_DateI, Student_DateN = @Student_DateN, Student_LieuN = @Student_LieuN, Student_Phone = @Student_Phone, Student_PhoneT = @Student_PhoneT, Student_DernierE = @Student_DernierE, Student_Email = @Student_Email, Student_NiveauD = @Student_NiveauD, Student_SerieNo = @Student_SerieNo, Student_Address = @Student_Address, Student_NiveauS = @Student_NiveauS, Student_Picture = @Student_Picture WHERE Student_ID = @Student_ID";
                    SqlConnection connection = IPS.GetConnection(connectionString);
                    SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add("@Student_ID", SqlDbType.Int).Value = (object)ID;
                    sqlCommand.Parameters.Add("@Class_ID", SqlDbType.Int).Value = (object)IPS.GetId("SELECT Class_ID FROM Class_Table WHERE Class_Name = '" + ClassName + "';", connectionString);
                    sqlCommand.Parameters.Add("@Student_Name", SqlDbType.VarChar).Value = (object)Nom;
                    sqlCommand.Parameters.Add("@Student_Prenom", SqlDbType.VarChar).Value = (object)Prenom;
                    sqlCommand.Parameters.Add("@Student_CIN", SqlDbType.VarChar).Value = (object)CIN;
                    sqlCommand.Parameters.Add("@Student_Massar", SqlDbType.VarChar).Value = (object)Massar;
                    sqlCommand.Parameters.Add("@Student_No", SqlDbType.VarChar).Value = (object)RegN;
                    sqlCommand.Parameters.Add("@Student_DateI", SqlDbType.VarChar).Value = (object)RegD;
                    sqlCommand.Parameters.Add("@Student_DateN", SqlDbType.VarChar).Value = (object)DN;
                    sqlCommand.Parameters.Add("@Student_LieuN", SqlDbType.VarChar).Value = (object)LN;
                    sqlCommand.Parameters.Add("@Student_Phone", SqlDbType.VarChar).Value = (object)Pho;
                    sqlCommand.Parameters.Add("@Student_PhoneT", SqlDbType.VarChar).Value = (object)PhoT;
                    sqlCommand.Parameters.Add("@Student_DernierE", SqlDbType.VarChar).Value = (object)DE;
                    sqlCommand.Parameters.Add("@Student_Email", SqlDbType.VarChar).Value = (object)Email;
                    sqlCommand.Parameters.Add("@Student_NiveauD", SqlDbType.VarChar).Value = (object)ND;
                    sqlCommand.Parameters.Add("@Student_SerieNo", SqlDbType.VarChar).Value = (object)SN;
                    sqlCommand.Parameters.Add("@Student_Address", SqlDbType.VarChar).Value = (object)adress;
                    sqlCommand.Parameters.Add("@Student_NiveauS", SqlDbType.VarChar).Value = (object)NS;
                    sqlCommand.Parameters.Add("@Student_Picture", SqlDbType.VarBinary).Value = (object)PictureData;
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Modification Effectuée!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error! \n" + ex.ToString(), "Student update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }

        public static bool DeleteStudent(string SID, string connectionString)
        {
            string cmdText = "DELETE FROM Student_Table WHERE Student_ID = @StudentID";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.Int).Value = (object)SID;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Suppression Effectuer!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Student delete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            connection.Close();
            return true;
        }
        public static bool AddStage(
            string StudentName,
            string NomS,
            string Dure,
            string DateD,
            string DateF,
            string NoteT,
            string NoteP,
            string Nature,
            string Hopital,
            string connectionString)
        {
            string cmdText = "INSERT INTO Stage_Table (Student_ID, Stage_Name, Stage_Duration, Stage_DateD, Stage_DateF, Stage_NoteT, Stage_NoteP, Stage_Moy, Stage_Nature, Stage_Hopital) VALUES (@Student_ID, @Stage_Name, @Stage_Duration, @Stage_DateD, @Stage_DateF, @Stage_NoteT, @Stage_NoteP, @Stage_Moy, @Stage_Nature, @Stage_Hopital)";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@Student_ID", SqlDbType.Int).Value = (object)IPS.GetId("SELECT Student_ID FROM Student_Table WHERE Student_Name = '" + StudentName + "';", connectionString);
            sqlCommand.Parameters.Add("@Stage_Name", SqlDbType.VarChar).Value = (object)NomS;
            sqlCommand.Parameters.Add("@Stage_Duration", SqlDbType.VarChar).Value = (object)Dure;
            sqlCommand.Parameters.Add("@Stage_DateD", SqlDbType.Date).Value = (object)DateTime.Parse(DateD);
            sqlCommand.Parameters.Add("@Stage_DateF", SqlDbType.Date).Value = (object)DateTime.Parse(DateF);
            sqlCommand.Parameters.Add("@Stage_NoteT", SqlDbType.Float).Value = (object)float.Parse(NoteT);
            sqlCommand.Parameters.Add("@Stage_NoteP", SqlDbType.Float).Value = (object)float.Parse(NoteP);
            sqlCommand.Parameters.Add("@Stage_Moy", SqlDbType.Float).Value = (float.Parse(NoteT) + float.Parse(NoteP))/2;
            sqlCommand.Parameters.Add("@Stage_Nature", SqlDbType.VarChar).Value = (object)Nature;
            sqlCommand.Parameters.Add("@Stage_Hopital", SqlDbType.VarChar).Value = (object)Hopital;
        try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Ajouter Avec Succes!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                connection.Close();
                return true;
            }
        catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Student add", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                connection.Close();
                return false;
            }
        }
        public static bool UpdateStage(
            string ID,
            string StudentName,
            string NomS,
            string Dure,
            string DateD,
            string DateF,
            string NoteT,
            string NoteP,
            string Nature,
            string Hopital,
            string connectionString)
        {
            string cmdText = "UPDATE Stage_Table SET Student_ID = @Student_ID, Stage_Name = @Stage_Name, Stage_Duration = @Stage_Duration, Stage_DateD = @Stage_DateD, Stage_DateF = @Stage_DateF, Stage_NoteT = @Stage_NoteT, Stage_NoteP = @Stage_NoteP, Stage_Moy = @Stage_Moy, Stage_Nature = @Stage_Nature, Stage_Hopital = @Stage_Hopital Where Stage_ID = @Stage_ID";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@Stage_ID", SqlDbType.Int).Value = (object)ID;
            sqlCommand.Parameters.Add("@Student_ID", SqlDbType.Int).Value = (object)IPS.GetId("SELECT Student_ID FROM Student_Table WHERE Student_Name = '" + StudentName + "';", connectionString);
            sqlCommand.Parameters.Add("@Stage_Name", SqlDbType.VarChar).Value = (object)NomS;
            sqlCommand.Parameters.Add("@Stage_Duration", SqlDbType.VarChar).Value = (object)Dure;
            sqlCommand.Parameters.Add("@Stage_DateD", SqlDbType.Date).Value = (object)DateTime.Parse(DateD);
            sqlCommand.Parameters.Add("@Stage_DateF", SqlDbType.Date).Value = (object)DateTime.Parse(DateF);
            sqlCommand.Parameters.Add("@Stage_NoteT", SqlDbType.Float).Value = (object)float.Parse(NoteT);
            sqlCommand.Parameters.Add("@Stage_NoteP", SqlDbType.Float).Value = (object)float.Parse(NoteP);
            sqlCommand.Parameters.Add("@Stage_Moy", SqlDbType.Float).Value = (float.Parse(NoteT) + float.Parse(NoteP)) / 2;
            sqlCommand.Parameters.Add("@Stage_Nature", SqlDbType.VarChar).Value = (object)Nature;
            sqlCommand.Parameters.Add("@Stage_Hopital", SqlDbType.VarChar).Value = (object)Hopital;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Modifier Avec Succes!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Stage Update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                connection.Close();
                return false;
            }
        }
        public static bool DeleteStage(string SID, string connectionString)
        {
            string cmdText = "DELETE FROM Stage_Table WHERE Stage_ID = @Stage_ID";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@Stage_ID", SqlDbType.Int).Value = (object)SID;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Suppression Effectuer!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Stage delete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            connection.Close();
            return true;
        }
        public static void MarkAttendance(
          string SID,
          string date,
          string status,
          string connectionString)
        {
            string cmdText = "INSERT INTO Attendance_Table VALUES (@date, @status, @Id)";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@date", SqlDbType.VarChar).Value = (object)date;
            sqlCommand.Parameters.Add("@status", SqlDbType.VarChar).Value = (object)status;
            sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = (object)SID;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Mark Attendance", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            connection.Close();
        }

        public static bool IsMarkAttendance(string date, string className, string connectionString)
        {
            string cmdText = "SELECT Student_Table.Student_ID, Student_Name, Student_No, Attendance_Status FROM Student_Table INNER JOIN Attendance_Table ON Student_Table.Student_ID = Attendance_Table.Student_ID INNER JOIN Class_Table ON Class_Table.Class_ID = Student_Table.Class_ID WHERE Attendance_Date = '" + date + "' AND Class_Name = '" + className + "';";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(cmdText, connection));
            DataTable dataTable = new DataTable();
            try
            {
                sqlDataAdapter.Fill(dataTable);
                connection.Close();
                if (dataTable.Rows.Count > 0)
                    return true;
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Date and Class", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return false;
        }
        public static bool IsPaymentExist(string paymentType, string paymentMonth, string connectionString)
        {
            string cmdText = "SELECT * FROM Payment_Table WHERE Payment_Type = '" + paymentType + "' AND Payment_Month = '" + paymentMonth + "';";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(cmdText, connection));
            DataTable dataTable = new DataTable();
            try
            {
                sqlDataAdapter.Fill(dataTable);
                connection.Close();
                if (dataTable.Rows.Count > 0)
                    return true;
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Payment Type and Month", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return false;
        }

        public static void UpdateAttendance(
          string SID,
          string date,
          string status,
          string connectionString)
        {
            string cmdText = "UPDATE Attendance_Table SET Attendance_Date = @date, Attendance_Status = @status, Student_ID = @SID WHERE Student_ID = @SID AND Attendance_Date = @date";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@SID", SqlDbType.Int).Value = (object)SID;
            sqlCommand.Parameters.Add("@status", SqlDbType.VarChar).Value = (object)status;
            sqlCommand.Parameters.Add("@date", SqlDbType.VarChar).Value = (object)date;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Attendance update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            connection.Close();
        }
        public static bool AddPayment(
          string studentname,
          string paymentType,
          string paymentNo,
          string paymentMonth,
          string paymentDate,
          string paymentAmount,
          string paymentDescription,
          string connectionString)
            {
            string cmdText = "INSERT INTO Payment_Table (Payment_Type, Payment_No, Payment_Month, Payment_Date, Payment_Amount, Payment_Description, Student_ID) VALUES (@Payment_Type, @Payment_No, @Payment_Month, @Payment_Date, @Payment_Amount, @Payment_Description, @Student_ID)";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@Payment_Type", SqlDbType.VarChar).Value = (object) paymentType;
            sqlCommand.Parameters.Add("@Payment_No", SqlDbType.Int).Value = (object) paymentNo;
            sqlCommand.Parameters.Add("@Payment_Month", SqlDbType.VarChar).Value = (object) paymentMonth;
            sqlCommand.Parameters.Add("@Payment_Date", SqlDbType.VarChar).Value = (object) paymentDate;
            sqlCommand.Parameters.Add("@Payment_Amount", SqlDbType.VarChar).Value = (object) paymentAmount;
            sqlCommand.Parameters.Add("@Payment_Description", SqlDbType.VarChar).Value = (object) paymentDescription;
            sqlCommand.Parameters.Add("@Student_ID", SqlDbType.Int).Value = (object) IPS.GetId("SELECT Student_ID FROM Student_Table WHERE Student_Name = '" + studentname + "';", connectionString);
            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Ajout Avec Succes!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Facture add", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }
        public static bool UpdatePayment(
          string studentId,
          string studentname,
          string paymentType,
          string paymentNo,
          string paymentMonth,
          string paymentDate,
          string paymentAmount,
          string paymentDescription,
          string connectionString)
        {
            string cmdText = "UPDATE Payment_Table SET Payment_Type = @Payment_Type, Payment_No = @Payment_No, Payment_Month = @Payment_Month, Payment_Date = @Payment_Date, Payment_Amount = @Payment_Amount, Payment_Description = @Payment_Description, Student_ID = @Student_ID WHERE Payment_ID = @Payment_ID";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@Payment_ID", SqlDbType.Int).Value = (object)studentId;
            sqlCommand.Parameters.Add("@Payment_Type", SqlDbType.VarChar).Value = (object)paymentType;
            sqlCommand.Parameters.Add("@Payment_No", SqlDbType.Int).Value = (object)paymentNo;
            sqlCommand.Parameters.Add("@Payment_Month", SqlDbType.VarChar).Value = (object)paymentMonth;
            sqlCommand.Parameters.Add("@Payment_Date", SqlDbType.VarChar).Value = (object)paymentDate;
            sqlCommand.Parameters.Add("@Payment_Amount", SqlDbType.VarChar).Value = (object)paymentAmount;
            sqlCommand.Parameters.Add("@Payment_Description", SqlDbType.VarChar).Value = (object)paymentDescription;
            sqlCommand.Parameters.Add("@Student_ID", SqlDbType.Int).Value = (object)IPS.GetId("SELECT Student_ID FROM Student_Table WHERE Student_Name = '" + studentname + "';", connectionString);
            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Ajout Avec Succes!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Facture add", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public static bool DeletePayment(string SID, string connectionString)
        {
            string cmdText = "DELETE FROM Payment_Table WHERE Payment_ID = @Payment_ID";
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@Payment_ID", SqlDbType.Int).Value = (object)SID;
            try
            {
                sqlCommand.ExecuteNonQuery();
                int num = (int)MessageBox.Show("Suppression Effectuer!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Student delete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            connection.Close();
            return true;
        }

        public static int Count(string query, string connectionString)
        {
            int num = 0;
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            try
            {
                return (int)sqlCommand.ExecuteScalar();
            }
            catch (SqlException )
            {

            }
            connection.Close();
            return num;
        }

        public static string GetUsernamePassword(string query, string connectionString)
        {
            SqlConnection connection = IPS.GetConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            try
            {
                return sqlCommand.ExecuteScalar().ToString();
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Error! \n" + ex.ToString(), "Username and password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            connection.Close();
            return "0";
        }
    }
}

