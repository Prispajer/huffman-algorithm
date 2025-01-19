import { instance } from "@viz-js/viz";


// Function to render a graph (in DOT format) as an SVG and display it in a specific HTML element
window.vizRenderGraph = function (data) {
    // Wait for the Viz.js instance to initialize and then render the SVG graph
    instance().then(viz => {
        // Render the graph data (DOT format) into an SVG element
        const svg = viz.renderSVGElement(data);

        // Clear any existing content inside the target HTML element
        document.getElementById("huffman-data-processing-component-summary-huffman-tree").innerHTML = "";

        // Append the generated SVG graph to the target HTML element
        document.getElementById("huffman-data-processing-component-summary-huffman-tree").appendChild(svg);
    });
}
