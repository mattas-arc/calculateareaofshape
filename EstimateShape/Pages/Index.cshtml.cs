using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstimateShape.Models;

namespace EstimateShape.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string SelectedShapeTag { get; set; }

        public SelectList TagShapes { get; set; }

        [BindProperty]
        public double AreaOfShape { get; set; }

        [BindProperty]
        public Shape Shape { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            TagShapes = new SelectList(GetShapesList(), nameof(ShapeListItem.Value), nameof(ShapeListItem.Text));
            return Page();
        }

        public IActionResult OnPost()
        {
            if(TagShapes== null  || !TagShapes.Any())
                TagShapes = new SelectList(GetShapesList(), nameof(ShapeListItem.Value), nameof(ShapeListItem.Text));
            if(SelectedShapeTag.Equals("Square"))
                AreaOfShape = AreaofSquare(Shape.Side);
            else if (SelectedShapeTag.Equals("Rectangle"))
                AreaOfShape = AreaofRectangle(Shape.Length,Shape.Width);
            else if (SelectedShapeTag.Equals("Circle"))
                AreaOfShape = AreaofCicrle(Shape.Radius);
            else if (SelectedShapeTag.Equals("Triangle"))
                AreaOfShape = AreaofTriangle(Shape.Base, Shape.Height);

            return Page();
        }

        private IEnumerable<ShapeListItem> GetShapesList()
        {
            return new List<ShapeListItem>
                {
                    new ShapeListItem{ Value = "Square", Text = "Square"},
                    new ShapeListItem{ Value = "Rectangle", Text = "Rectangle"},
                    new ShapeListItem{ Value = "Triangle", Text = "Triangle"},
                    new ShapeListItem{ Value = "Circle", Text = "Circle"}
                };
        }

        private double AreaofSquare(double side)
        {
            return  Math.Pow(side,2);
        }

        private double AreaofRectangle(double length,double width)
        {
            return length * width;
        }

        private double AreaofCicrle(double radius)
        {
            return Math.PI * Math.Pow(radius, 2);
        }

        private double AreaofTriangle(double ba,double height)
        {
            return (ba*height)/2;
        }
    }
}
