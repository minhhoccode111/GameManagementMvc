// using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Models
{
    /*
       Nguyên nhân MovieGenreViewModel không được tạo bởi EF:
       Không phải là thực thể cơ sở dữ liệu:
       MovieGenreViewModel là một ViewModel, được thiết kế để chứa dữ liệu từ nhiều nguồn khác nhau và truyền chúng đến view. Nó không đại diện cho một bảng trong cơ sở dữ liệu.

       ViewModel được tạo thủ công:
       ViewModel thường được tạo thủ công bởi lập trình viên để phù hợp với yêu cầu cụ thể của ứng dụng. Chúng thường kết hợp dữ liệu từ nhiều bảng hoặc nguồn khác nhau, và chứa các thuộc tính không trực tiếp ánh xạ tới các cột trong cơ sở dữ liệu.

       Entity Framework chỉ tạo các lớp thực thể (Entity Classes):
       EF chỉ tạo các lớp thực thể từ các bảng trong cơ sở dữ liệu khi bạn sử dụng công cụ scaffold để tạo mã từ mô hình cơ sở dữ liệu.

       MovieGenreViewModel là một ViewModel được tạo thủ công để chứa dữ liệu cần thiết cho view, và không được tạo tự động bởi Entity Framework vì nó không đại diện cho một thực thể cơ sở dữ liệu. Điều này là hoàn toàn bình thường và là một phần của thiết kế ứng dụng MVC để tách biệt dữ liệu và logic hiển thị.
       */
    public class MovieGenreViewModel
    {
        // a list of movies
        public List<Movie>? Movies { get; set; }

        // SelectList: Represents a list that lets users select a single item\.
        // This class is typically rendered as an HTML <select> element with the
        // specified collection of `SelectListItem` objects
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
